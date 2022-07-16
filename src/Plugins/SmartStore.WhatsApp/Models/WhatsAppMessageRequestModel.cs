using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.WhatsApp.Models
{
    public class WhatsAppMessageRequestModel
    {
        [JsonProperty("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("template")]
        public Template Template { get; set; }
              
    }

    public class Component
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("parameters")]
        public List<Parameter> Parameters { get; set; }

        [JsonProperty("sub_type")]
        public string SubType { get; set; }

        [JsonProperty("index")]
        public int? Index { get; set; }
    }

    public class Image
    {
        [JsonProperty("link")]
        public string Link { get; set; }
    }

    public class Language
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class Parameter
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("image")]
        public Image Image { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }



    public class Template
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public Language Language { get; set; }

        [JsonProperty("components")]
        public List<Component> Components { get; set; }
    }

}