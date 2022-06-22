using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.DellyManLogistics.Models
{
    public class GetQuoteRequestModel
    {
        [JsonProperty("CustomerID")]
        public int CustomerID { get; set; }

        [JsonProperty("PaymentMode")]
        public string PaymentMode { get; set; }

        [JsonProperty("VehicleID")]
        public int VehicleID { get; set; }

        [JsonProperty("PickupRequestedTime")]
        public string PickupRequestedTime { get; set; }

        [JsonProperty("PickupRequestedDate")]
        public string PickupRequestedDate { get; set; }

        [JsonProperty("PickupAddress")]
        public string PickupAddress { get; set; }

        [JsonProperty("DeliveryAddress")]
        public List<string> DeliveryAddress { get; set; }

        [JsonProperty("ProductAmount")]
        public List<decimal> ProductAmount { get; set; }

        [JsonProperty("PackageWeight")]
        public List<int> PackageWeight { get; set; }

        [JsonProperty("IsProductOrder")]
        public int IsProductOrder { get; set; }

        [JsonProperty("IsProductInsurance")]
        public int IsProductInsurance { get; set; }

        [JsonProperty("InsuranceAmount")]
        public int InsuranceAmount { get; set; }

        [JsonProperty("IsInstantDelivery")]
        public int IsInstantDelivery { get; set; }
    }
}