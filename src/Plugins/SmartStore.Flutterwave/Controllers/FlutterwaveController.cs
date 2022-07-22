using Newtonsoft.Json;
using SmartStore.ComponentModel;
using SmartStore.Core;
using SmartStore.Core.Domain.Orders;
using SmartStore.Core.Logging;
using SmartStore.Flutterwave.Models;
using SmartStore.Flutterwave.Settings;
using SmartStore.Services;
using SmartStore.Services.Common;
using SmartStore.Services.Customers;
using SmartStore.Services.Orders;
using SmartStore.Services.Payments;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;

namespace SmartStore.Controllers
{
    public class FlutterwaveController : PaymentControllerBase
    {
        private readonly ICommonServices _services;
        private readonly IGenericAttributeService _genericAttributeService;

        private readonly ILogger logger;
        private readonly IPaymentService paymentService;
        private readonly IOrderService orderService;
        private readonly IOrderProcessingService orderProcessingService;
        private readonly IWorkContext _workContext;
        private readonly OrderSettings _orderSettings;
        private readonly IStoreContext _storeContext;

        public FlutterwaveController(
            ICommonServices services,
             ILogger logger,
            IGenericAttributeService genericAttributeService,
             IPaymentService paymentService,
            IOrderService orderService,
            IOrderProcessingService orderProcessingService,
            IWorkContext workContext,
            OrderSettings orderSettings,
            IStoreContext storeContext)
        {
            _services = services;
            _genericAttributeService = genericAttributeService;

            this.logger = logger;
            this.paymentService = paymentService;
            this.orderService = orderService;
            this.orderProcessingService = orderProcessingService;
            _workContext = workContext;
            _orderSettings = orderSettings;
            _storeContext = storeContext;
        }


        [AdminAuthorize]
        [ChildActionOnly]
        [LoadSetting]
        public ActionResult Configure(FlutterwaveSettings settings)
        {
            var model = new ConfigurationModel();
            MiniMapper.Map(settings, model);

            return View(model);
        }


        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        [SaveSetting]
        public ActionResult Configure(FlutterwaveSettings settings, ConfigurationModel model, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                return Configure(settings);
            }


            MiniMapper.Map(model, settings);
            return RedirectToConfiguration("SmartStore.Flutterwave", false);
        }




        /// <summary>
        /// This method will be called when the payment method was selected and the button for the next checkout page was clicked
        /// </summary>
        [NonAction]
        public override IList<string> ValidatePaymentForm(FormCollection form)
        {
            // Sample for how to validate plugin elements in the checkout process
            var warnings = new List<string>();
            var str = form["UseAndAgreeToThirdPartyTerms"].EmptyNull().ToLower();
            var useAndAgree = str.Contains("on") || str.Contains("true");

            if (!useAndAgree)
            {
                warnings.Add(T("Plugins.SmartStore.Flutterwave.Agreement.Message"));
            }

            return warnings;
        }

        /// <summary>
        /// Creates the ProcessPaymentRequest object and returns it where needed.
        /// Can be used to transmit sensitive values entered by customer during checkout (like credit card data) to the ProcessPaymentRequest.
        /// </summary>
		[NonAction]
        public override ProcessPaymentRequest GetPaymentInfo(FormCollection form)
        {
            var paymentInfo = new ProcessPaymentRequest
            {
                CreditCardNumber = form["CreditCardNumber"],
                // etc.
            };

            return paymentInfo;
        }

        /// <summary>
        /// This action will render the payment option into the payment selection page in the frontend checkout process
        /// </summary>
        public ActionResult PaymentInfo()
        {
            ViewBag.AgreementText = T("Plugins.SmartStore.Flutterwave.Agreement").Text;

            return PartialView();
        }

        [ValidateInput(false)]
        public ActionResult PaymentConfirmation(string status, string tx_ref,string transaction_id)
        {

            var settings = _services.Settings.LoadSetting<FlutterwaveSettings>(_services.StoreContext.CurrentStore.Id);
            if (!status.Equals("successful", StringComparison.InvariantCultureIgnoreCase))
            {
                return RedirectToFailedPage(status, tx_ref);
            }
            using (var httpclient = new HttpClient())
            {

                httpclient.BaseAddress = new Uri(settings.BaseUrl);
                httpclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.PrivateKey);

                string url = $"/{settings.ApiVersion}/transactions/{transaction_id}/verify";

                var request = httpclient.GetAsync(url).Result;
                var responseContent = request.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<FlutterwaveResponseModel<VerifyTransactionResponseModel>>(responseContent);

                var order = GetOrder(tx_ref);

                if (result.Status.Equals("success", StringComparison.InvariantCultureIgnoreCase) 
                    && result.Data.Status.Equals("successful", StringComparison.InvariantCultureIgnoreCase)
                    && result.Data.Amount >= order.OrderTotal)
                {

                    if (order.AuthorizationTransactionId.IsEmpty())
                    {
                        order.AuthorizationTransactionId = order.CaptureTransactionId = tx_ref;
                        order.AuthorizationTransactionResult = order.CaptureTransactionResult = result.Data.Status;

                        orderService.UpdateOrder(order);
                    }

                    if (orderProcessingService.CanMarkOrderAsPaid(order))
                    {
                        orderProcessingService.MarkOrderAsPaid(order);
                    }
                    return RedirectToAction("Completed", "Checkout", new { area = "" });
                }
                else
                {
                    return RedirectToFailedPage(result.Message, tx_ref);
                }

            }
            // var guid = Guid.TryParse
            // var order = orderService.GetOrderByGuid(reference);

        }

        ActionResult RedirectToFailedPage(string message,string tx_ref)
        {
            return RedirectToAction("FailedPayment", "Flutterwave", new { area = "SmartStore.Flutterwave", orderGuid = tx_ref, status = message });
        }


        //[ValidateInput(false)]
        //[HttpPost]
        //public ActionResult WebHookCallBack(PaystackWebhook model)
        //{
        //    try
        //    {
        //        var webhookString = JsonConvert.SerializeObject(model);
        //        logger.Log(LogLevel.Information, null, webhookString, null);
        //        var settings = _services.Settings.LoadSetting<PaystackSettings>(_services.StoreContext.CurrentStore.Id);

        //        return new HttpStatusCodeResult(200);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Log(LogLevel.Error, ex, null, null);
        //        return new HttpStatusCodeResult(500);
        //    }

        //}

        public ActionResult FailedPayment(string orderGuid, string status)
        {

            //validation
            if ((_workContext.CurrentCustomer.IsGuest() && !_orderSettings.AnonymousCheckoutAllowed))
                return new HttpUnauthorizedResult();

            //model
            var model = new PaymentFailedModel();


            var order = GetOrder(orderGuid);
            //var order = orderService.SearchOrders(_storeContext.CurrentStore.Id, _workContext.CurrentCustomer.Id,
            //     null, null, null, null, null, null,orderGuid,null, 0, 1).FirstOrDefault();

            if (order == null || order.Deleted || _workContext.CurrentCustomer.Id != order.CustomerId)
            {
                return HttpNotFound();
            }

            if (order.AuthorizationTransactionId.IsEmpty())
            {
                order.AuthorizationTransactionId = order.CaptureTransactionId = orderGuid;
                order.AuthorizationTransactionResult = order.CaptureTransactionResult = status;

                orderService.UpdateOrder(order);
            }

            model.OrderId = order.Id;
            model.OrderNumber = order.GetOrderNumber();

            orderService.AddOrderNote(order, status, true);

            //  return RedirectToAction("Index", "Home", new { area = "" });

            return View(model);

        }

        Order GetOrder(string referenceId)
        {
            var orderGuidNumber = Guid.Parse(referenceId);
            var order = orderService.GetOrderByGuid(orderGuidNumber);

            return order;
        }
    }
}