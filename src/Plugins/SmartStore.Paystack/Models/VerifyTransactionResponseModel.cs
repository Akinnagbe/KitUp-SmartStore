using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Paystack.Models
{
    public class VerifyTransactionResponseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("message")]
        public object Message { get; set; }

        [JsonProperty("gateway_response")]
        public string GatewayResponse { get; set; }

        [JsonProperty("paid_at")]
        public DateTime PaidAt { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        //[JsonProperty("log")]
        //public Log Log { get; set; }

        [JsonProperty("fees")]
        public int Fees { get; set; }

        [JsonProperty("fees_split")]
        public object FeesSplit { get; set; }

        //[JsonProperty("authorization")]
        //public Authorization Authorization { get; set; }

        //[JsonProperty("customer")]
        //public Customer Customer { get; set; }

        [JsonProperty("plan")]
        public object Plan { get; set; }

        //[JsonProperty("split")]
        //public Split Split { get; set; }

        [JsonProperty("order_id")]
        public object OrderId { get; set; }


        [JsonProperty("requested_amount")]
        public int RequestedAmount { get; set; }

        [JsonProperty("pos_transaction_data")]
        public object PosTransactionData { get; set; }

        [JsonProperty("source")]
        public object Source { get; set; }

        [JsonProperty("transaction_date")]
        public DateTime TransactionDate { get; set; }

        //[JsonProperty("plan_object")]
        //public PlanObject PlanObject { get; set; }

        //[JsonProperty("subaccount")]
        //public Subaccount Subaccount { get; set; }
    }
}