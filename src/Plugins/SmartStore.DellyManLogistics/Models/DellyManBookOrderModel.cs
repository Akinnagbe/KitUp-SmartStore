using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.DellyManLogistics.Models
{
    public class DellyManBookOrderModel
    {
        public DellyManBookOrderModel()
        {
            Packages = new List<DellyManPackageModel>();
        }
        [JsonProperty("CustomerID")]
        public int CustomerID { get; set; }

        [JsonProperty("CompanyID")]
        public int CompanyID { get; set; }

        [JsonProperty("PaymentMode")]
        public string PaymentMode { get; set; }

        [JsonProperty("Vehicle")]
        public string Vehicle { get; set; }

        [JsonProperty("PickUpContactName")]
        public string PickUpContactName { get; set; }

        [JsonProperty("PickUpContactNumber")]
        public string PickUpContactNumber { get; set; }

        [JsonProperty("PickUpGooglePlaceAddress")]
        public string PickUpGooglePlaceAddress { get; set; }

        [JsonProperty("PickUpLandmark")]
        public string PickUpLandmark { get; set; }

        [JsonProperty("IsInstantDelivery")]
        public int IsInstantDelivery { get; set; }

        [JsonProperty("PickUpRequestedDate")]
        public string PickUpRequestedDate { get; set; }

        [JsonProperty("PickUpRequestedTime")]
        public string PickUpRequestedTime { get; set; }

        [JsonProperty("DeliveryRequestedTime")]
        public string DeliveryRequestedTime { get; set; }

        [JsonProperty("DeliveryRequestedDate")]
        public string DeliveryRequestedDate { get; set; }

        [JsonProperty("Packages")]
        public List<DellyManPackageModel> Packages { get; set; }
    }
}