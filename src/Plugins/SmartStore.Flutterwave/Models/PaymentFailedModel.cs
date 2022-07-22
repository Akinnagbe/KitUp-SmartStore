using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartStore.Flutterwave.Models
{
    public class PaymentFailedModel
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public string OrderNumber { get; set; }
    }
}