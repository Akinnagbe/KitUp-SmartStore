using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.DellyManLogistics.Models
{
    public class DellyManCompanyModel
    {
        [JsonProperty("CompanyID")]
        public int CompanyID { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("TotalPrice")]
        public decimal TotalPrice { get; set; }

        [JsonProperty("OriginalPrice")]
        public int OriginalPrice { get; set; }

        [JsonProperty("SavedPrice")]
        public int SavedPrice { get; set; }

        [JsonProperty("PayablePrice")]
        public int PayablePrice { get; set; }

        [JsonProperty("DeductablePrice")]
        public int DeductablePrice { get; set; }

        [JsonProperty("AvgRating")]
        public int AvgRating { get; set; }

        [JsonProperty("NumberOfOrders")]
        public int NumberOfOrders { get; set; }

        [JsonProperty("NumberOfRating")]
        public int NumberOfRating { get; set; }
    }
}