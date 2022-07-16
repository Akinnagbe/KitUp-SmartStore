using FluentValidation;
using FluentValidation.Attributes;
using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SmartStore.WhatsApp.Models
{
    [Validator(typeof(ConfigurationModelValidator))]
    public class ConfigurationModel : ModelBase
    {


        [SmartResourceDisplayName("Plugins.SmartStore.WhatsApp.BaseUrl")]
        public string BaseUrl { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.WhatsApp.Version")]
        public string Version { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.WhatsApp.AccessToken")]
        public string AccessToken { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.WhatsApp.PhoneNumberId")]
        public string PhoneNumberId { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.WhatsApp.Template")]
        public string Template { get; set; }

        [SmartResourceDisplayName("Plugins.SmartStore.WhatsApp.DefaultPhoneNumber")]
        public string DefaultPhoneNumber { get; set; }


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
        //[SmartResourceDisplayName("Plugins.SmartStore.WhatsApp.PictureId")]
        //public int PictureId { get; set; }

        ///// <summary>
        ///// Renders a color picker control onto a string property
        ///// </summary>
        //[SmartResourceDisplayName("Plugins.SmartStore.WhatsApp.Color")]
        //[UIHint("Color")]
        //public string Color { get; set; }

        ///// <summary>
        ///// For simple validation (like string length or required) you can annotate the property respectivly with attributes as shown below
        ///// </summary>
        //[StringLength(3)]
        ////[Required]
        //[AllowHtml]
        //[SmartResourceDisplayName("Plugins.SmartStore.WhatsApp.Text")]
        //public string Text { get; set; }

        //#endregion
    }


    /// <summary>
    /// For more complex validation you can write your own validators. 
    /// For more information visit https://fluentvalidation.net/
    /// </summary>
    public class ConfigurationModelValidator : AbstractValidator<ConfigurationModel>
    {
        public ConfigurationModelValidator()
        {
            RuleFor(x => x.BaseUrl).NotEmpty();
            RuleFor(x => x.Version).NotEmpty();
            RuleFor(x => x.PhoneNumberId).NotEmpty();
            RuleFor(x => x.AccessToken).NotEmpty();
            RuleFor(x => x.DefaultPhoneNumber).NotEmpty();
        }
    }
}