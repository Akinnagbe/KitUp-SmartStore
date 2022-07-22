using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Flutterwave.Models
{
    public class VerifyTransactionResponseModel
    {
        public VerifyTransactionResponseModel()
        {
            Customer = new Dictionary<string, string>();
        }
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("tx_ref")]
        public string TxRef { get; set; }

        [JsonProperty("flw_ref")]
        public string FlwRef { get; set; }

        [JsonProperty("device_fingerprint")]
        public string DeviceFingerprint { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("charged_amount")]
        public decimal ChargedAmount { get; set; }

        [JsonProperty("app_fee")]
        public decimal AppFee { get; set; }

        [JsonProperty("merchant_fee")]
        public decimal MerchantFee { get; set; }

        [JsonProperty("processor_response")]
        public string ProcessorResponse { get; set; }

        [JsonProperty("auth_model")]
        public string AuthModel { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("narration")]
        public string Narration { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("payment_type")]
        public string PaymentType { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("amount_settled")]
        public decimal AmountSettled { get; set; }

        [JsonProperty("customer")]
        public Dictionary<string,string> Customer { get; set; }
    }
}