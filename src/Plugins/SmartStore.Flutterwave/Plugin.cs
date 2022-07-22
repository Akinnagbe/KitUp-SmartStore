using SmartStore.Core.Plugins;
using SmartStore.Flutterwave;
using SmartStore.Flutterwave.Settings;
using SmartStore.Services;
using SmartStore.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Web.Routing;




namespace SmartStore.Flutterwave
{
    public class Plugin : BasePlugin
    {
        private readonly ISettingService _settingService;
        private readonly ICommonServices _services;


        public Plugin(ISettingService settingService,
            ICommonServices services)
        {
            _settingService = settingService;
            _services = services;

        }







        public override void Install()
        {
            // Save settings with default values.
            _services.Settings.SaveSetting(new FlutterwaveSettings());

            // Import localized plugin resources (you can edit or add these in /Localization/resources.[Culture].xml).
            _services.Localization.ImportPluginResourcesFromXml(this.PluginDescriptor);


            base.Install();
        }

        public override void Uninstall()
        {
            _services.Settings.DeleteSetting<FlutterwaveSettings>();
            _services.Localization.DeleteLocaleStringResources(this.PluginDescriptor.ResourceRootKey);


            base.Uninstall();
        }
    }
}
