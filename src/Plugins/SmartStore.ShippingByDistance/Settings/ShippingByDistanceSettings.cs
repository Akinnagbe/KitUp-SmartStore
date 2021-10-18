using SmartStore.Core.Configuration;

namespace SmartStore.ShippingByDistance.Settings
{
    public class ShippingByDistanceSettings : ISettings
    {
       

        public bool IsFixedRate { get; set; }
        public decimal FixedAmount { get; set; }
        public string PackageManApiBaseUrl { get; set; }


        //public int PictureId { get; set; }
        //public string Color { get; set; }
        //public string Text { get; set; }




    }
}