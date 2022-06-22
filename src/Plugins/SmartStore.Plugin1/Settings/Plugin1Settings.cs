using SmartStore.Core.Configuration;

namespace SmartStore.Plugin1.Settings
{
    public class Plugin1Settings : ISettings
    {
        public string MyFirstSetting { get; set; }


        public int PictureId { get; set; }
        public string Color { get; set; }
        public string Text { get; set; }




    }
}