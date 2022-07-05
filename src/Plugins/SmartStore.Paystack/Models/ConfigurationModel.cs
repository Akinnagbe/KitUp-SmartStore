using FluentValidation;
using FluentValidation.Attributes;
using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SmartStore.Paystack.Models
{
   // [Validator(typeof(ConfigurationModelValidator))]
    public class ConfigurationModel : ModelBase
    {
        [Required]
       // [Url]
        public string BaseUrl { get; set; }

        [Required]
        public string PublicKey { get; set; }

        [Required]
        public string PrivateKey { get; set; }

        public bool SupportsRefund { get; set; }

        public string CallBackUrl { get; set; }

        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.MyFirstSetting")]
        //[AllowHtml]
        //public string MyFirstSetting { get; set; }

        //[SmartResourceDisplayName("Plugins.Payments.Paystack.UseSandbox")]
        //public bool UseSandbox { get; set; }

        //[SmartResourceDisplayName("Plugins.Payments.Paystack.AccountId")]
        //public string AccountId { get; set; }

        //[SmartResourceDisplayName("Plugins.Payments.Paystack.AccountPassword")]
        //public string AccountPassword { get; set; }

        [SmartResourceDisplayName("Plugins.Payments.Paystack.AdditionalFee")]
        public decimal AdditionalFee { get; set; }

        [SmartResourceDisplayName("Plugins.Payments.Paystack.AdditionalFeePercentage")]
        public bool AdditionalFeePercentage { get; set; }

        [SmartResourceDisplayName("Plugins.Payments.Paystack.Fee")]
        [Required]
        public decimal Fee { get; set; }

        //#region Sample properties

        ///// <summary>
        ///// Smartstore has implemented several controls to configure certain recurring plugin properties like picture, color or html texts.
        ///// These controls are implemented as EditorTemplates and can be found at the the following two locations
        ///// src\Presentation\SmartStore.Web\Views\Shared\EditorTemplates
        ///// src\Presentation\SmartStore.Web\Administration\Views\Shared\EditorTemplates
        ///// 
        ///// To render a control in your plugin configuration page whereby a shop admin can upload a picture (picture editor template) 
        ///// you can just annotate an int property as shown below.
        ///// </summary>
        //[UIHint("Picture")]
        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.PictureId")]
        //public int PictureId { get; set; }

        ///// <summary>
        ///// Renders a color picker control onto a string property
        ///// </summary>
        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Color")]
        //[UIHint("Color")]
        //public string Color { get; set; }

        ///// <summary>
        ///// For simple validation (like string length or required) you can annotate the property respectivly with attributes as shown below
        ///// </summary>
        //[StringLength(3)]
        ////[Required]
        //[AllowHtml]
        //[SmartResourceDisplayName("Plugins.SmartStore.Paystack.Text")]
        //public string Text { get; set; }

        //#endregion
    }


    /// <summary>
    /// For more complex validation you can write your own validators. 
    /// For more information visit https://fluentvalidation.net/
    /// </summary>
    //public class ConfigurationModelValidator : AbstractValidator<ConfigurationModel>
    //{
    //    public ConfigurationModelValidator()
    //    {
    //       // RuleFor(x => x.MyFirstSetting).NotEmpty();
    //    }
    //}
}