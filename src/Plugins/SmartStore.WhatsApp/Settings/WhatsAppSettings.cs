using SmartStore.Core.Configuration;

namespace SmartStore.WhatsApp.Settings
{
    public class WhatsAppSettings : ISettings
    {
        public string BaseUrl { get; set; }

        public string Template { get; set; }

        public string Version { get; set; }

        public string PhoneNumberId { get; set; }

        public string AccessToken { get; set; }
        public string DefaultPhoneNumber { get; set; }
    }
}