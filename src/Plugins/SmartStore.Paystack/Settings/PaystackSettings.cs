using SmartStore.Core.Configuration;

namespace SmartStore.Paystack.Settings
{
    public class PaystackSettings : ISettings
    {
      
        public string BaseUrl { get; set; }

        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }

        public bool SupportsRefund { get; set; }

        public string CallBackUrl { get; set; }
        public decimal AdditionalFee { get; set; }
        public bool AdditionalFeePercentage { get; set; }
        public decimal Fee { get; set; }

    }
}