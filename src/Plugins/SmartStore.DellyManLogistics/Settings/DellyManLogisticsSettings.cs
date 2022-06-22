using SmartStore.Core.Configuration;

namespace SmartStore.DellyManLogistics.Settings
{
    public class DellyManLogisticsSettings : ISettings
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerId { get; set; }
        public string PickupRequestedTime { get; set; }
        public string OrderTrackingUrl { get; set; }
    }
}