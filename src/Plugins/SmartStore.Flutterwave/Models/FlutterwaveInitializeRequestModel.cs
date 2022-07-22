using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Flutterwave.Models
{
    public class FlutterwaveInitializeRequestModel
    {
        [JsonProperty("tx_ref")]
        public string TxRef { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }

        [JsonProperty("meta")]
        public Dictionary<string,string> Meta { get; set; }

        [JsonProperty("customer")]
        public Dictionary<string, string> Customer { get; set; }

        [JsonProperty("customizations")]
        public Dictionary<string, string> Customizations { get; set; }
    }
}