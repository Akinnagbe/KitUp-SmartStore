using Newtonsoft.Json;
using SmartStore.Core.Domain.Shipping;
using SmartStore.Core.Localization;
using SmartStore.Core.Logging;
using SmartStore.Core.Plugins;
using SmartStore.DellyManLogistics.Models;
using SmartStore.DellyManLogistics.Settings;
using SmartStore.Services;
using SmartStore.Services.Configuration;
using SmartStore.Services.Localization;
using SmartStore.Services.Shipping;
using SmartStore.Services.Shipping.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace SmartStore.DellyManLogistics.Providers
{
    [SystemName("Shipping.DellyManDistanceRate")]
    [FriendlyName("Distance Rate Shipping", Description = "Distance Rate Shipping")]
    [DisplayOrder(0)]
    public class DistanceRateProvider : IShippingRateComputationMethod, IConfigurable
    {

        private readonly ISettingService _settingService;
        private readonly IShippingService _shippingService;
        protected readonly ICommonServices _services;

        public DistanceRateProvider(ISettingService settingService,
             ICommonServices services,
            IShippingService shippingService)
        {
            this._settingService = settingService;
            _services = services;
            this._shippingService = shippingService;


            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }
        public ShippingRateComputationMethodType ShippingRateComputationMethodType => ShippingRateComputationMethodType.Realtime;

        public IShipmentTracker ShipmentTracker => null;

        public bool IsActive => true;

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "DellyManLogistics";
            routeValues = new RouteValueDictionary() { { "area", "SmartStore.DellyManLogistics" } };
        }

        public decimal? GetFixedRate(GetShippingOptionRequest getShippingOptionRequest)
        {
            return 10m;
        }

        public GetShippingOptionResponse GetShippingOptions(GetShippingOptionRequest getShippingOptionRequest)
        {
            if (getShippingOptionRequest == null)
                throw new ArgumentNullException("getShippingOptionRequest");

            var response = new GetShippingOptionResponse();

            if (getShippingOptionRequest.Items == null || getShippingOptionRequest.Items.Count == 0)
            {
                response.AddError(T("Admin.System.Warnings.NoShipmentItems"));
                return response;
            }

            var shippingMethod = this._shippingService.GetShippingMethodById(4);

            // var cartAmount = getShippingOptionRequest.Items.Sum()


            var shippingOption = new ShippingOption();
            shippingOption.ShippingMethodId = shippingMethod.Id;
            shippingOption.Name = shippingMethod.GetLocalized(x => x.Name);
            shippingOption.Description = shippingMethod.GetLocalized(x => x.Description);
            shippingOption.Rate = GetRate(getShippingOptionRequest);
            response.ShippingOptions.Add(shippingOption);
            //}

            return response;
        }

        private decimal GetRate(GetShippingOptionRequest getShippingOptionRequest)
        {
            var store = _services.StoreContext.CurrentStore;
            var settings = _services.Settings.LoadSetting<DellyManLogisticsSettings>(_services.StoreContext.CurrentStore.Id);
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(settings.BaseUrl);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.ApiKey);

                httpClient.DefaultRequestHeaders
                      .Accept
                      .Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var getQuoteRequestModel = new GetQuoteRequestModel
                {
                    CustomerID = int.Parse(settings.CustomerId),
                    DeliveryAddress = new List<string> { getShippingOptionRequest.ShippingAddress.Address1 },
                    InsuranceAmount = 0,
                    IsInstantDelivery = 0,
                    IsProductInsurance = 0,
                    IsProductOrder = 0,
                    PackageWeight = new List<int>(),
                    PaymentMode = "pickup",
                    PickupAddress = settings.DefaultPickUpGoogleAddress,
                    PickupRequestedDate = DateTime.Now.ToString("dd/MM/yyyy"),
                    PickupRequestedTime = settings.PickupRequestedTime,
                    ProductAmount = new List<decimal> { },
                    VehicleID = 1

                };
                var jsonRequest = JsonConvert.SerializeObject(getQuoteRequestModel);
                Logger.InfoFormat("DellyMan Request for Customer {0} is {1}", getShippingOptionRequest.Customer.Username, jsonRequest);

                var stringContent = new StringContent(jsonRequest, Encoding.UTF8);
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var request = httpClient.PostAsync("/api/v3.0/GetQuotes", stringContent).Result;
                var content = request.Content.ReadAsStringAsync().Result;
                Logger.InfoFormat("DellyMan Response for Customer {0} is {1}", getShippingOptionRequest.Customer.Username, content);

                var response = JsonConvert.DeserializeObject<DellyManBaseResponseModel<List<DellyManCompanyModel>>>(content);
                if (response.ResponseCode == 100 && response.ResponseMessage.Equals("Success", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (response.Data != null && response.Data.Count > 0)
                    {
                        var company = response.Data.FirstOrDefault();
                        if (string.IsNullOrEmpty(settings.CompanyId) || company.CompanyID.ToString() != settings.CompanyId)
                        {
                            settings.CompanyId = company.CompanyID.ToString();
                            _services.Settings.SaveSetting<DellyManLogisticsSettings>(settings, _services.StoreContext.CurrentStore.Id);
                        }

                        return company.TotalPrice;
                    }

                    return settings.DefaultDeliveryFee;
                }
                else
                {
                    return settings.DefaultDeliveryFee;
                }


            }


        }
    }
}