using SmartStore.Core.Plugins;
using SmartStore.Paystack;
using SmartStore.Paystack.Settings;
using SmartStore.Services;
using SmartStore.Services.Configuration;
using SmartStore.Services.Tasks;
using System;
using System.Collections.Generic;
using System.Web.Routing;

namespace SmartStore.Paystack
{
    public class Plugin : BasePlugin
    {
        private readonly ISettingService _settingService;
        private readonly ICommonServices _services;
        private readonly IScheduleTaskService _scheduleTaskService;

        public Plugin(ISettingService settingService,
            ICommonServices services,
            IScheduleTaskService scheduleTaskService)
        {
            _settingService = settingService;
            _services = services;
            _scheduleTaskService = scheduleTaskService;
        }







        public override void Install()
        {
            // Save settings with default values.
            _services.Settings.SaveSetting(new PaystackSettings());

            // Import localized plugin resources (you can edit or add these in /Localization/resources.[Culture].xml).
            _services.Localization.ImportPluginResourcesFromXml(this.PluginDescriptor);

            _scheduleTaskService.GetOrAddTask<MyFirstTask>(x =>
            {
                x.Name = "SmartStore.Paystack.Tasks.MyFirstTask";
                x.CronExpression = "*/30 * * * *"; // every 30 min.
                x.Enabled = true;
                x.RunPerMachine = true;
            });

            base.Install();
        }

        public override void Uninstall()
        {
            _services.Settings.DeleteSetting<PaystackSettings>();
            _services.Localization.DeleteLocaleStringResources(this.PluginDescriptor.ResourceRootKey);

            _scheduleTaskService.TryDeleteTask<MyFirstTask>();

            base.Uninstall();
        }
    }
}
