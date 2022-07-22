using SmartStore.Core.Configuration;

namespace SmartStore.Flutterwave.Settings
{
    public class FlutterwaveSettings : ISettings
    {
        public string BaseUrl { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public bool SupportsRefund { get; set; }       
        public string ApiVersion { get; set; }
        public string EncryptionKey { get; set; }
    }
}