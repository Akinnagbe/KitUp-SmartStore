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
        //public string MyFirstSetting { get; set; }


        //public int PictureId { get; set; }
        //public string Color { get; set; }
        //public string Text { get; set; }




        //public bool UseSandbox { get; set; }
        //public string AccountId { get; set; }
        //public string AccountPassword { get; set; }
        //public decimal AdditionalFee { get; set; }
        //public bool AdditionalFeePercentage { get; set; }

    }
}