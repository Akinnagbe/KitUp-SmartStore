using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace SmartStore.DellyManLogistics.Models
{
    public class ConfigurationModel : ModelBase
    {
        public ConfigurationModel()
        {
            AvailableCities = new List<SelectListItem>();
            AvailableStateProvincies = new List<SelectListItem>();
        }

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

        //[SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.CompanyId")]       
        //public string CompanyId { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.DefaultDeliveryFee")]
        [Required]
        public decimal DefaultDeliveryFee { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.DefaultPickUpContactName")]
        [Required]
        public string DefaultPickUpContactName { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.DefaultPickUpContactNumber")]
        [Required]
        public string DefaultPickUpContactNumber { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.DefaultPickUpGoogleAddress")]
        [Required]
        public string DefaultPickUpGoogleAddress { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.DefaultPickUpCity")]
        [Required]
        public string DefaultPickUpCity { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.DefaultPickUpState")]
        [Required]
        public string DefaultPickUpState { get; set; }

        public List<SelectListItem> AvailableCities { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.DellyManLogistics.DefaultPickUpState")]
        public List<SelectListItem> AvailableStateProvincies { get; set; }
    }


}