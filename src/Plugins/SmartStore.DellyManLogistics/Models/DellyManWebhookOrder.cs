using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.DellyManLogistics.Models
{
    public class DellyManWebhookOrder
    {
        [JsonProperty("OrderID")]
        public string OrderID { get; set; }

        [JsonProperty("OrderCode")]
        public string OrderCode { get; set; }

        [JsonProperty("CustomerID")]
        public string CustomerID { get; set; }

        [JsonProperty("CompanyID")]
        public string CompanyID { get; set; }

        [JsonProperty("TrackingID")]
        public string TrackingID { get; set; }

        [JsonProperty("OrderDate")]
        public string OrderDate { get; set; }

        [JsonProperty("OrderStatus")]
        public string OrderStatus { get; set; }

        [JsonProperty("OrderPrice")]
        public string OrderPrice { get; set; }

        [JsonProperty("AssignedAt")]
        public string AssignedAt { get; set; }

        [JsonProperty("PickedUpAt")]
        public string PickedUpAt { get; set; }

        [JsonProperty("DeliveredAt")]
        public string DeliveredAt { get; set; }
    }
}