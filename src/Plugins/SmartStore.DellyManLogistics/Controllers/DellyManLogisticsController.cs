using Newtonsoft.Json;
using SmartStore.ComponentModel;
using SmartStore.Core.Data;
using SmartStore.Core.Domain.Orders;
using SmartStore.Core.Domain.Shipping;
using SmartStore.Core.Logging;
using SmartStore.DellyManLogistics.Models;
using SmartStore.DellyManLogistics.Settings;
using SmartStore.Services;
using SmartStore.Services.Common;
using SmartStore.Services.Shipping;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Security;
using SmartStore.Web.Framework.Settings;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;



namespace SmartStore.Controllers
{
    public class DellyManLogisticsController : AdminControllerBase
    {
        private readonly ICommonServices _services;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IRepository<Shipment> _shipmentRepository;
        private readonly IRepository<Order> _orderRepository;

        public DellyManLogisticsController(
            ICommonServices services,
            IRepository<Shipment> shipmentRepository,
            IRepository<Order> orderRepository,
            IGenericAttributeService genericAttributeService)
        {
            _services = services;
            _genericAttributeService = genericAttributeService;
            _shipmentRepository = shipmentRepository;
            _orderRepository = orderRepository;
        }


        [AdminAuthorize]
        [ChildActionOnly]
        [LoadSetting]
        public ActionResult Configure(DellyManLogisticsSettings settings)
        {
            var model = new ConfigurationModel();
            MiniMapper.Map(settings, model);

            return View(model);
        }


        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        [SaveSetting]
        public ActionResult Configure(DellyManLogisticsSettings settings, ConfigurationModel model, FormCollection form)
        {
            if (!ModelState.IsValid)
            {
                return Configure(settings);
            }


            MiniMapper.Map(model, settings);
            return RedirectToConfiguration("SmartStore.DellyManLogistics");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult OrderStatusCallback(DellyManWebhookBaseModel model)
        {
            try
            {

                var webhookString = JsonConvert.SerializeObject(model);
                Logger.Log(LogLevel.Information, null, webhookString, null);
                var settings = _services.Settings.LoadSetting<DellyManLogisticsSettings>(_services.StoreContext.CurrentStore.Id);

                var shipment = _shipmentRepository.Table.FirstOrDefault(s => s.TrackingNumber == model.Order.TrackingID);

                if (shipment != null)
                {
                    var order = _orderRepository.GetById(shipment.OrderId);
                    switch (model.Order.OrderStatus)
                    {
                        case "PENDING":
                            order.OrderStatus = Core.Domain.Orders.OrderStatus.Pending;
                            break;
                        case "ASSIGNED":
                            order.OrderStatus = Core.Domain.Orders.OrderStatus.Processing;
                            break;
                        case "INTRANSIT":
                            order.OrderStatus = Core.Domain.Orders.OrderStatus.Processing;
                            shipment.ShippedDateUtc = DateTime.TryParse(model.Order.PickedUpAt, out DateTime pickedUpAt) ? pickedUpAt : default(DateTime?);
                            break;
                        case "COMPLETED":
                            order.OrderStatus = Core.Domain.Orders.OrderStatus.Complete;
                            shipment.DeliveryDateUtc = DateTime.TryParse(model.Order.DeliveredAt, out DateTime deliveredAT) ? deliveredAT : default(DateTime?);
                            break;
                        case "CANCELLED":
                            order.OrderStatus = Core.Domain.Orders.OrderStatus.Cancelled;
                            break;
                        default:
                            break;
                    }

                    _shipmentRepository.Update(shipment);
                    _orderRepository.Update(order);
                }

                return new HttpStatusCodeResult(200);
            }
            catch (Exception ex)
            {
                Logger.Log(LogLevel.Error, ex, null, null);
                return new HttpStatusCodeResult(500);
            }
        }


    }
}