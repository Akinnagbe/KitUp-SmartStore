using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Flutterwave.Models
{
    public class FlutterwaveResponseModel<T>
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}