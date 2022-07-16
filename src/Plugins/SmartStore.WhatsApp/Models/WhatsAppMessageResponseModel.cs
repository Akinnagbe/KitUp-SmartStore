using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.WhatsApp.Models
{
    public class WhatsAppMessageResponseModel
    {
        public WhatsAppMessageResponseModel()
        {
            Contacts = new List<Contact>();
            Messages = new List<Message>();
        }
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }
    public class Contact
    {
        [JsonProperty("input")]
        public string Input { get; set; }

        [JsonProperty("wa_id")]
        public string WaId { get; set; }
    }

    public class Message
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }

}