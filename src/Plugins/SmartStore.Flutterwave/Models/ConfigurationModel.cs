using FluentValidation;
using FluentValidation.Attributes;
using SmartStore.Web.Framework;
using SmartStore.Web.Framework.Modelling;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SmartStore.Flutterwave.Models
{
    [Validator(typeof(ConfigurationModelValidator))]
    public class ConfigurationModel : ModelBase
    {
       // [Required]
        [SmartResourceDisplayName("Plugins.Payments.Flutterwave.BaseUrl")]
        public string BaseUrl { get; set; }

       // [Required]
        [SmartResourceDisplayName("Plugins.Payments.Flutterwave.PublicKey")]
        public string PublicKey { get; set; }

       // [Required]
        [SmartResourceDisplayName("Plugins.Payments.Flutterwave.PrivateKey")]
        public string PrivateKey { get; set; }

        
        [SmartResourceDisplayName("Plugins.Payments.Flutterwave.SupportsRefund")]
        public bool SupportsRefund { get; set; }

       // [Required]
        //[StringLength(2)]
        [SmartResourceDisplayName("Plugins.Payments.Flutterwave.ApiVersion")]
        public string ApiVersion { get; set; }

       // [Required]
        [SmartResourceDisplayName("Plugins.Payments.Flutterwave.EncryptionKey")]
        public string EncryptionKey { get; set; }

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
            RuleFor(x => x.PublicKey).NotEmpty();
            RuleFor(x => x.PrivateKey).NotEmpty();
            RuleFor(x => x.ApiVersion)
                .NotEmpty()
                .MaximumLength(3);
        }
    }
}