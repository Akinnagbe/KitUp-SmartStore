using Newtonsoft.Json;
using SmartStore.Controllers;
using SmartStore.Core.Domain.Orders;
using SmartStore.Core.Domain.Payments;
using SmartStore.Core.Logging;
using SmartStore.Core.Plugins;
using SmartStore.Flutterwave.Models;
using SmartStore.Flutterwave.Settings;
using SmartStore.Services;
using SmartStore.Services.Orders;
using SmartStore.Services.Payments;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Routing;

namespace SmartStore.Flutterwave
{
    [SystemName("SmartStore.Flutterwave")]
    [FriendlyName("Pay with Flutterwave")]
    [DisplayOrder(1)]
    public class PaymentProvider : PaymentMethodBase, IConfigurable
    {
        protected readonly ICommonServices _services;
        protected readonly IOrderTotalCalculationService _orderTotalCalculationService;

        public PaymentProvider(
            ICommonServices services,
            IOrderTotalCalculationService orderTotalCalculationService)
        {
            _services = services;
            _orderTotalCalculationService = orderTotalCalculationService;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public static string SystemName => "SmartStore.Flutterwave";

        /// <summary>
        /// Defines whether the payment can be catpured by this payment provider.
        /// </summary>
		public override bool SupportCapture => false;

        /// <summary>
        /// Defines whether the payment can be parially refunded by this payment provider.
        /// </summary>
		public override bool SupportPartiallyRefund => false;

        /// <summary>
        /// Defines whether the payment can be refunded by this payment provider.
        /// </summary>
		public override bool SupportRefund => IsRefundSupported();

        /// <summary>
        /// Defines whether the payment can be voided by this payment provider.
        /// </summary>
		public override bool SupportVoid => false;

        /// <summary>
        /// Must be true when the payment method requests payment data during checkout.
        /// </summary>
        public override bool RequiresInteraction => false;

        /// <summary>
        /// For more information about supported payment types and there function make a right click on PaymentMethodType and go to definition.
        /// </summary>
        public override PaymentMethodType PaymentMethodType => PaymentMethodType.Redirection;

        bool IsRefundSupported()
        {
            var settings = _services.Settings.LoadSetting<FlutterwaveSettings>(_services.StoreContext.CurrentStore.Id);
            return settings.SupportsRefund;
        }
        /// <summary>
        /// Will process the payment. Is always executed after confirm order in the checkout process was clicked.
        /// </summary>
        public override ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new ProcessPaymentResult();
            result.NewPaymentStatus = PaymentStatus.Pending;

            // implement process payment
            var settings = _services.Settings.LoadSetting<FlutterwaveSettings>(processPaymentRequest.StoreId);

            if (settings.BaseUrl.IsEmpty())
            {
                result.AddError(T("Plugins.Payments.Flutterwave.InvalidBaseUrl"));
            }
            if (settings.PublicKey.IsEmpty())
            {
                result.AddError(T("Plugins.Payments.Flutterwave.InvalidPublicKey"));
            }
            if (settings.PrivateKey.IsEmpty())
            {
                result.AddError(T("Plugins.Payments.Flutterwave.InvalidPrivateKey"));
            }

            return result;
        }

        /// <summary>
        /// Optional: Will be called before payment is processed
        /// </summary>
        public override PreProcessPaymentResult PreProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            var result = new PreProcessPaymentResult();

            return result;
        }

        /// <summary>
        /// Will be called after payment is processed
        /// </summary>
        public override void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            if (postProcessPaymentRequest.Order.PaymentStatus == PaymentStatus.Paid)
                return;

            var redirectUrl = InitializePayment(postProcessPaymentRequest, postProcessPaymentRequest.Order.OrderGuid);
            postProcessPaymentRequest.RedirectUrl = redirectUrl;
        }

        string InitializePayment(PostProcessPaymentRequest postProcessPaymentRequest, Guid referenceNumber)
        {
            var store = _services.StoreContext.CurrentStore;
            var settings = _services.Settings.LoadSetting<FlutterwaveSettings>(_services.StoreContext.CurrentStore.Id);
            var returnUrl = _services.WebHelper.GetStoreLocation(store.SslEnabled) + "Plugins/Flutterwave/PaymentConfirmation";

            using (var httpclient = new HttpClient())
            {
                var requestModel = new FlutterwaveInitializeRequestModel
                {
                    Amount = postProcessPaymentRequest.Order.OrderTotal.ToString("#####"),
                    TxRef = referenceNumber.ToString("n"),
                    Currency = "NGN",
                    Customer = new Dictionary<string, string>
                    {
                        {"email",postProcessPaymentRequest.Order.Customer.Email },
                        {"phonenumber",postProcessPaymentRequest.Order.Customer.CustomerNumber },
                        {"fullname",postProcessPaymentRequest.Order.Customer.FullName }
                    },
                    RedirectUrl = returnUrl,
                    Meta = new Dictionary<string, string>
                    {
                        {"orderId", postProcessPaymentRequest.Order.Id.ToString()},
                        {"orderGuid",  referenceNumber.ToString("n")}
                    }
                };

                httpclient.BaseAddress = new Uri(settings.BaseUrl);
                httpclient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.PrivateKey);
                string json = JsonConvert.SerializeObject(requestModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string endpoint = string.Format("/{0}/payments", settings.ApiVersion);
                var request = httpclient.PostAsync(endpoint, content).Result;
                var responseContent = request.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<FlutterwaveResponseModel<FlutterwaveInitializeResponseModel>>(responseContent);

                return result.Data.Link;
            }

            // var request = Refit.RestService.For<IPaystackService>(settings.BaseUrl).InitializePayment(requestModel, settings.PrivateKey).Result;

            //  return request.Data.AuthorizationUrl;
        }



        /// <summary>
        /// Will be called on payment selection page in checkout process.
        /// </summary>
        /// <returns>Payment fee</returns>
        //public override decimal GetAdditionalHandlingFee(IList<OrganizedShoppingCartItem> cart)
        //{
        //    var result = decimal.Zero;
        //    try
        //    {
        //        var settings = _services.Settings.LoadSetting<FlutterwaveSettings>(_services.StoreContext.CurrentStore.Id);

        //        result = this.CalculateAdditionalFee(_orderTotalCalculationService, cart, settings.AdditionalFee, settings.AdditionalFeePercentage);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(ex);
        //    }
        //    return result;
        //}

        /// <summary>
        /// When true user can reprocess payment from MyAccount > Orders > OrderDetail
        /// </summary>
        public override bool CanRePostProcessPayment(Order order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            if (order.PaymentStatus == PaymentStatus.Pending && (DateTime.UtcNow - order.CreatedOnUtc).TotalSeconds > 5)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Defines payment info route.
        /// </summary>
		public override void GetPaymentInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PaymentInfo";
            controllerName = "Flutterwave";
            routeValues = new RouteValueDictionary() { { "area", "SmartStore.Flutterwave" } };
        }

        public override Type GetControllerType()
        {
            return typeof(FlutterwaveController);
        }

        /// <summary>
        /// Defines provider configuration route.
        /// </summary>
		public override void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "Flutterwave";
            routeValues = new RouteValueDictionary() { { "area", "SmartStore.Flutterwave" } };
        }

        /// <summary>
        /// Will be executed when payment is captured by shop admin in the backend.
        /// </summary>
        public override CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            var order = capturePaymentRequest.Order;
            var result = new CapturePaymentResult
            {
                NewPaymentStatus = order.PaymentStatus
            };

            try
            {
                var settings = _services.Settings.LoadSetting<FlutterwaveSettings>(order.StoreId);

                var data = new Dictionary<string, object>
                {
                    { "amount",  order.OrderTotal.FormatInvariant() }
                };

                // Pseudo  code
                //var response = CallPaymentProviderApi_CaptureRequest(JsonConvert.SerializeObject(data));

                //if (response.Success)
                //{
                //    result.NewPaymentStatus = PaymentStatus.Paid;
                //    result.CaptureTransactionId = (string)response.Json.id;
                //    result.CaptureTransactionResult = (string)response.Json.processing.shortId;
                //}
                //else
                //{
                //    result.AddError(response.MerchantErrors.FirstOrDefault() ?? T("Admin.Common.UnknownError"));
                //    Log(LogLevel.Error, data, response);
                //}

                //if (settings.AddOrderNotes)
                //{
                //      TODO: add order notes
                //}
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
                Logger.Error(ex);
            }

            return result;
        }

        /// <summary>
        /// Will be executed when payment is refunded by shop admin in the backend.
        /// </summary>
        public override RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            var order = refundPaymentRequest.Order;
            var result = new RefundPaymentResult
            {
                NewPaymentStatus = order.PaymentStatus
            };

            try
            {
                var settings = _services.Settings.LoadSetting<FlutterwaveSettings>(order.StoreId);

                // Pseudo  code
                //var response = CallPaymentProviderApi_CaptureRequest(refundPaymentRequest.AmountToRefund);
                //if (response.success)
                //{
                //    result.NewPaymentStatus = refundPaymentRequest.IsPartialRefund ? PaymentStatus.PartiallyRefunded : PaymentStatus.Refunded;
                //}
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
                Logger.Error(ex);
            }

            return result;
        }

        /// <summary>
        /// Will be executed when payment is voided by shop admin in the backend.
        /// </summary>
        public override VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            var order = voidPaymentRequest.Order;
            var result = new VoidPaymentResult
            {
                NewPaymentStatus = order.PaymentStatus
            };

            try
            {
                var settings = _services.Settings.LoadSetting<FlutterwaveSettings>(order.StoreId);

                // Pseudo code
                //var response = CallPaymentProviderApi_VoidRequest(order.OrderTotal);
                //if (response.success)
                //{
                //    result.NewPaymentStatus = PaymentStatus.Voided;
                //}
            }
            catch (Exception ex)
            {
                result.AddError(ex.Message);
                Logger.Error(ex);
            }

            return result;
        }
    }
}