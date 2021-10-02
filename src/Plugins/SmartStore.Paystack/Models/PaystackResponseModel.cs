using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartStore.Paystack.Models
{
    public class PaystackResponseModel<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}