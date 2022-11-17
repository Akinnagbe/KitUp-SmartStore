﻿using SmartStore.Core.Configuration;

namespace SmartStore.DellyManLogistics.Settings
{
    public class DellyManLogisticsSettings : ISettings
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerId { get; set; }
        public string CompanyId { get; set; }
        public string PickupRequestedTime { get; set; }
        public string OrderTrackingUrl { get; set; }
        public decimal DefaultDeliveryFee { get; set; }
        public string DefaultPickUpContactName { get; set; }
        public string DefaultPickUpContactNumber { get; set; }
        public string DefaultPickUpGoogleAddress { get; set; }
        public string DefaultPickUpCity { get; set; }
        public string DefaultPickUpState { get; set; }
    }
}