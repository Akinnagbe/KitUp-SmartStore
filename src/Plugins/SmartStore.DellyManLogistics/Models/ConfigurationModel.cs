using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace SmartStore.DellyManLogistics.Models
{
    public class ConfigurationModel : ModelBase
    {


        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.ApiKey")]
        // [AllowHtml]
        [Required]
        public string ApiKey { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.BaseUrl")]
        [Required]
        public string BaseUrl { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.CustomerCode")]
        [Required]
        public string CustomerCode { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.CustomerId")]
        [Required]
        public string CustomerId { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.PickupRequestedTime")]
        [Required]
        [Display(Name = "Pickup Request Time")]
        public string PickupRequestedTime { get; set; } = "06 AM to 09 PM";

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.OrderTrackingUrl")]
        [Required]
        [Display(Name = "Order Tracking Url")]
        public string OrderTrackingUrl { get; set; }
    }


}