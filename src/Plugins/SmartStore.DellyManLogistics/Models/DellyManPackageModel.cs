using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.DellyManLogistics.Models
{
    public class DellyManPackageModel
    {
        [JsonProperty("PackageDescription")]
        public string PackageDescription { get; set; }

        [JsonProperty("DeliveryContactName")]
        public string DeliveryContactName { get; set; }

        [JsonProperty("DeliveryContactNumber")]
        public string DeliveryContactNumber { get; set; }

        [JsonProperty("DeliveryGooglePlaceAddress")]
        public string DeliveryGooglePlaceAddress { get; set; }

        [JsonProperty("DeliveryLandmark")]
        public string DeliveryLandmark { get; set; }

        [JsonProperty("PickUpState")]
        public string PickUpState { get; set; }

        [JsonProperty("PickUpCity")]
        public string PickUpCity { get; set; }

        [JsonProperty("DeliveryState")]
        public string DeliveryState { get; set; }

        [JsonProperty("DeliveryCity")]
        public string DeliveryCity { get; set; }
    }
}