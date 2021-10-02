using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Paystack.Models
{
    public class PaystackInitializeRequestModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("callback_url")]
        public string CallBackUrl { get; set; }

    }
}