using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using SmartStore.ComponentModel;
using SmartStore.Core.Domain.Messages;
using SmartStore.Core.Domain.Orders;
using SmartStore.Core.Events;
using SmartStore.Core.Logging;
using SmartStore.DellyManLogistics.Models;
using SmartStore.DellyManLogistics.Settings;
using SmartStore.Services;
using SmartStore.Services.Catalog;
using SmartStore.Services.Common;
using SmartStore.Services.Directory;
using SmartStore.Services.Localization;
using SmartStore.Services.Messages;
using SmartStore.Services.Orders;
using SmartStore.Services.Shipping;
using SmartStore.Templating;
using SmartStore.Utilities;

namespace SmartStore.DellyManLogistics.Events
{
    public class EvenConsumer : IConsumer
    {
        private static string[] _templateNames = new string[]
       {
            MessageTemplateNames.OrderCancelledCustomer,
            MessageTemplateNames.OrderCompletedCustomer,
            MessageTemplateNames.OrderPlacedCustomer,
            MessageTemplateNames.OrderPlacedStoreOwner
       };

        private readonly ICommonServices _services;
        private readonly HttpContextBase _httpContext;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IGenericAttributeService> _genericAttributeService;
        private readonly Lazy<ICurrencyService> _currencyService;
        private readonly Lazy<ILanguageService> _languageService;
        private readonly Lazy<IPriceFormatter> _priceFormatter;
        private readonly Lazy<IShipmentService> _shipmentService;

        public EvenConsumer(
            ICommonServices services,
            HttpContextBase httpContext,
            Lazy<IOrderService> orderService,
            Lazy<IGenericAttributeService> genericAttributeService,
            Lazy<ICurrencyService> currencyService,
            Lazy<ILanguageService> languageService,
            Lazy<IShipmentService> shipmentService,
            Lazy<IPriceFormatter> priceFormatter)
        {
            _services = services;
            _httpContext = httpContext;
            _orderService = orderService;
            _genericAttributeService = genericAttributeService;
            _currencyService = currencyService;
            _languageService = languageService;
            _priceFormatter = priceFormatter;
            _shipmentService = shipmentService;
            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public void HandleEvent(MessageModelCreatedEvent message)
        {
            if (!_templateNames.Contains(message.MessageContext.MessageTemplate.Name))
            {
                return;
            }

            try
            {


            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        public void HandleEvent(OrderPaidEvent eventMessage)
        {
            if (eventMessage?.Order == null)
                return;


            try
            {
                var settings = _services.Settings.LoadSetting<DellyManLogisticsSettings>(eventMessage.Order.StoreId);
                string customerUsername = eventMessage.Order.Customer.Username;
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(settings.BaseUrl);
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.ApiKey);

                    httpClient.DefaultRequestHeaders
                          .Accept
                          .Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    var bookOrder = new DellyManBookOrderModel
                    {
                        CompanyID = int.Parse(settings.CompanyId),
                        CustomerID = int.Parse(settings.CustomerId),
                        DeliveryRequestedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                        DeliveryRequestedTime = DateTime.Now.ToString("dd/MM/yyyy"),
                        IsInstantDelivery = 0,
                        Packages = eventMessage.Order.OrderItems.Select(item => new DellyManPackageModel
                        {
                            DeliveryCity = eventMessage.Order.Customer.ShippingAddress.City,
                            DeliveryContactName = eventMessage.Order.Customer.FullName,
                            DeliveryContactNumber = eventMessage.Order.ShippingAddress.PhoneNumber ,
                            DeliveryGooglePlaceAddress = eventMessage.Order.Customer.ShippingAddress.Address1,
                            DeliveryLandmark = "",
                            DeliveryState = eventMessage.Order.Customer.ShippingAddress.StateProvince.Name,
                            PackageDescription = item.Product.Name,
                            PickUpCity =settings.DefaultPickUpCity,
                            PickUpState = settings.DefaultPickUpState

                        }).ToList(),
                        PaymentMode = "pickup",
                        PickUpContactName = settings.DefaultPickUpContactName,
                        PickUpContactNumber =settings.DefaultPickUpContactNumber,
                        PickUpGooglePlaceAddress = settings.DefaultPickUpGoogleAddress,
                        PickUpLandmark = "",
                        PickUpRequestedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                        PickUpRequestedTime = settings.PickupRequestedTime,
                        Vehicle = "Bike"
                    };
                    var jsonRequest = JsonConvert.SerializeObject(bookOrder);
                    Logger.InfoFormat("DellyMan Request for Customer {0} is {1}", customerUsername, jsonRequest);

                    var stringContent = new StringContent(jsonRequest, Encoding.UTF8);
                    stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var request = httpClient.PostAsync("/api/v3.0/BookOrder", stringContent).Result;
                    var content = request.Content.ReadAsStringAsync().Result;
                    Logger.InfoFormat("DellyMan Response for Customer {0} is {1}", customerUsername, content);

                    var response = JsonConvert.DeserializeObject<DellyManBookOrderResponseModel>(content);
                    if (response.ResponseCode == 100 && response.ResponseMessage.Equals("Success", StringComparison.InvariantCultureIgnoreCase))
                    {
                        _shipmentService.Value.InsertShipment(new Core.Domain.Shipping.Shipment
                        {
                            TrackingNumber = response.TrackingID,
                            TrackingUrl = string.Format("{0}?id={1}", settings.OrderTrackingUrl, response.TrackingID),
                            OrderId = eventMessage.Order.Id,
                            CreatedOnUtc = DateTime.Now
                        });

                    }



                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }
    }
}