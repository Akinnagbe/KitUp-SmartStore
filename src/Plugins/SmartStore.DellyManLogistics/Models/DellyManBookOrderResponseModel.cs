using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.DellyManLogistics.Models
{
    public class DellyManBookOrderResponseModel
    {
        [JsonProperty("ResponseCode")]
        public int ResponseCode { get; set; }

        [JsonProperty("ResponseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("OrderID")]
        public string OrderID { get; set; }

        [JsonProperty("TrackingID")]
        public string TrackingID { get; set; }

        [JsonProperty("Reference")]
        public string Reference { get; set; }
    }
}