using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.DellyManLogistics.Models
{
    public class DellyManBaseResponseModel<T>
    {
        [JsonProperty("ResponseCode")]
        public int ResponseCode { get; set; }

        [JsonProperty("ResponseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty("Companies")]
        public T Data { get; set; }

        [JsonProperty("RejectedCompanies")]
        public DellyManRejectedCompany RejectedCompanies { get; set; }

        [JsonProperty("Products")]
        public DellyManProduct Products { get; set; }

        [JsonProperty("Distance")]
        public int Distance { get; set; }
    }


    public class DellyManRejectedCompany
    {

    }

    public class DellyManProduct
    {

    }
}