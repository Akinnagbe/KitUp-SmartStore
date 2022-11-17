﻿using SmartStore.ComponentModel;
using SmartStore.Licensing;
using SmartStore.Plugin1.Models;
using SmartStore.Plugin1.Settings;
using SmartStore.Services;
using SmartStore.Services.Common;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Settings;
using System.Web.Mvc;


namespace SmartStore.Controllers
{
    public class Plugin1Controller : AdminControllerBase
    {
        private readonly ICommonServices _services;
        private readonly IGenericAttributeService _genericAttributeService;

        public Plugin1Controller(
            ICommonServices services,
            IGenericAttributeService genericAttributeService)
        {
            _services = services;
            _genericAttributeService = genericAttributeService;
        }

        [LicenseRequired]
        [AdminAuthorize]
        [ChildActionOnly]
        [LoadSetting]
        public ActionResult Configure(Plugin1Settings settings)
        {
            var model = new ConfigurationModel();
            MiniMapper.Map(settings, model);



            return View(model);
        }

        [LicenseRequired]
        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        [SaveSetting]
        public ActionResult Configure(Plugin1Settings settings, ConfigurationModel model, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                return Configure(settings);
            }


            MiniMapper.Map(model, settings);
            return RedirectToConfiguration("SmartStore.Plugin1");
        }




        [AdminAuthorize]
        public ActionResult AdminEditTab(int entityId, string entityName)
        {
            var model = new AdminEditTabModel();

            // Simple values can be stored and obtained as GenericAttributes. More complex values should implement their own domain objects.
            // Get saved value from GenericAttribute. (Value persitence can be found in the ModelBoundEventConsumer.)
            model.IsActive = _genericAttributeService.GetAttribute<bool>(entityName, entityId, "Plugin1IsActive");
            model.EntityId = entityId;
            model.EntityName = entityName;

            var result = PartialView(model);
            result.ViewData.TemplateInfo = new TemplateInfo { HtmlFieldPrefix = "CustomProperties[Plugin1]" };
            return result;
        }

    }
}