using SmartStore.Core;
using SmartStore.Core.Events;
using SmartStore.Services.Catalog;
using SmartStore.Services.Common;
using SmartStore.Services.Configuration;
using SmartStore.ShippingByDistance.Models;
using SmartStore.ShippingByDistance.Settings;
using SmartStore.Web.Framework.Modelling;
using SmartStore.Web.Framework.Security;

namespace SmartStore.ShippingByDistance
{
    public class ModelBoundEventConsumer : IConsumer
    {
        private readonly ISettingService _settingService;
        private readonly IStoreContext _storeContext;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IProductService _productService;

        public ModelBoundEventConsumer(IStoreContext storeContext,
            ISettingService settingService,
            IGenericAttributeService genericAttributeService,
            IProductService productService)
        {
            _storeContext = storeContext;
            _settingService = settingService;
            _genericAttributeService = genericAttributeService;
            _productService = productService;
        }

        // This method will be executed when the model of the containing entity is bound.
        // You can use it to store the data the shop admin has entered in the injected tab.
        [AdminAuthorize]
        public void HandleEvent(ModelBoundEvent eventMessage)
        {
            if (!eventMessage.BoundModel.CustomProperties.ContainsKey("ShippingByDistance"))
                return;

            var model = eventMessage.BoundModel.CustomProperties["ShippingByDistance"] as AdminEditTabModel;
            if (model == null)
                return;

            var settings = _settingService.LoadSetting<ShippingByDistanceSettings>(_storeContext.CurrentStore.Id);

            var currentProduct = _productService.GetProductById(model.EntityId);


            // Simple values can be stored and obtained as GenericAttributes. More complex values should implement their own domain objects.
            // Persist value as GenericAttribute. (Value obtainment can be found in the controller.)
            _genericAttributeService.SaveAttribute<bool>(currentProduct, "ShippingByDistanceIsActive", model.IsActive);



        }
    }
}