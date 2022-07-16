using Newtonsoft.Json;
using SmartStore.Core;
using SmartStore.Core.Domain.Catalog;
using SmartStore.Core.Domain.Orders;
using SmartStore.Core.Events;
using SmartStore.Core.Logging;
using SmartStore.Services;
using SmartStore.Services.Catalog;
using SmartStore.Services.Common;
using SmartStore.Services.Configuration;
using SmartStore.Services.Directory;
using SmartStore.Services.Localization;
using SmartStore.Services.Orders;
using SmartStore.Services.Security;
using SmartStore.Services.Shipping;
using SmartStore.Web.Framework.Controllers;
using SmartStore.Web.Framework.Events;
using SmartStore.Web.Framework.Modelling;
using SmartStore.Web.Framework.Security;
using SmartStore.WhatsApp.Models;
using SmartStore.WhatsApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc.Html;

namespace SmartStore.WhatsApp
{
    public class EventConsumer : IConsumer
    {
        private readonly ICommonServices _services;
        private readonly IEventPublisher _eventPublisher;
        private readonly HttpContextBase _httpContext;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IGenericAttributeService> _genericAttributeService;
        private readonly Lazy<ICurrencyService> _currencyService;
        private readonly Lazy<ILanguageService> _languageService;
        private readonly Lazy<IPriceFormatter> _priceFormatter;
        private readonly Lazy<IShipmentService> _shipmentService;

        public EventConsumer(ICommonServices services,
            IEventPublisher eventPublisher,
            HttpContextBase httpContext,
            Lazy<IOrderService> orderService,
            Lazy<IGenericAttributeService> genericAttributeService,
            Lazy<ICurrencyService> currencyService,
            Lazy<ILanguageService> languageService,
            Lazy<IShipmentService> shipmentService,
            Lazy<IPriceFormatter> priceFormatter)
        {
            _services = services;
            _eventPublisher = eventPublisher;
            _httpContext = httpContext;
            _orderService = orderService;
            _genericAttributeService = genericAttributeService;
            _currencyService = currencyService;
            _languageService = languageService;
            _priceFormatter = priceFormatter;
            _shipmentService = shipmentService;
            Logger = NullLogger.Instance;
        }
        public ILogger Logger { get; set; }

        public void HandleEvent(OrderPlacedEvent eventMessage)
        {
            // Do something when an order is placed

            // Don't let the next line mislead you! This was only added for explanation purposes not to be actually implemented in this event.
            // If you want to know which other events there are, just make a right click on .Publish( and search for references in this solution.
            //_eventPublisher.Publish(new OrderPaidEvent(null));
        }

        public void HandleEvent(OrderPaidEvent eventMessage)
        {
            if (eventMessage?.Order == null)
                return;


            try
            {
                var settings = _services.Settings.LoadSetting<WhatsAppSettings>(eventMessage.Order.StoreId);
                string customerUsername = eventMessage.Order.Customer.FullName;
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(settings.BaseUrl);
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", settings.AccessToken);

                    httpClient.DefaultRequestHeaders
                          .Accept
                          .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var orderItems = eventMessage.Order.OrderItems;

                    foreach (var order in orderItems) 
                    {
                      var itemPicture =   order.Product.ProductPictures.FirstOrDefault();
                        if(itemPicture != null)
                        {
                            var media = itemPicture.MediaFile;
                            var pictureUrl = string.Format("{0}://{1}/media/{2}/catalog/{3}", _httpContext.Request.Url.Scheme, _httpContext.Request.Url.Authority, media.Id, media.Name);

                            var whatsAppModel = BuildWhatsAppModel(settings, order.Product.ProductVendor, order.Product.Name, pictureUrl, customerUsername);
                            var whatsAppJson = JsonConvert.SerializeObject(whatsAppModel);

                            Logger.InfoFormat("WhatsApp Request: {0}", whatsAppJson);

                            var stringContent = new StringContent(whatsAppJson, Encoding.UTF8);
                            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                            var endpoint = string.Format("/{0}/{1}/messages",settings.Version,settings.PhoneNumberId);
                            var request = httpClient.PostAsync(endpoint, stringContent).Result;
                            var content = request.Content.ReadAsStringAsync().Result;
                            Logger.InfoFormat("WhatsApp Response for Customer {0} is {1}", customerUsername, content);

                            var response = JsonConvert.DeserializeObject<WhatsAppMessageResponseModel>(content);
                            
                        }
                       
                    }
                    

                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }


        WhatsAppMessageRequestModel BuildWhatsAppModel(WhatsAppSettings whatsAppSettings, ProductVendor productVendor,string productName, string pictureUrl,string customerName)
        {
            try
            {
                var whatsAppModel = new WhatsAppMessageRequestModel
                {
                    MessagingProduct = "whatsapp",
                    To = productVendor !=null?productVendor.PhoneNumber:whatsAppSettings.DefaultPhoneNumber,
                    Type = "template",
                    Template = new Template
                    {
                        Name = whatsAppSettings.Template,
                        Language = new Language
                        {
                            Code = "en_GB"
                        },
                        Components = new List<Component>
                            {
                                 new Component
                                 {
                                       Type ="body",
                                       Parameters= new List<Parameter>
                                       {
                                            new Parameter
                                            {
                                                 Type= "text",
                                                 Text=productVendor !=null?productVendor.BusinessName:"Fashionable Wears"
                                            },
                                            new Parameter
                                            {
                                                 Type= "text",
                                                 Text=productName
                                            },
                                              new Parameter
                                            {
                                                 Type= "text",
                                                 Text=customerName
                                            },
                                               new Parameter
                                            {
                                                 Type= "text",
                                                 Text="1234567"
                                            }
                                       }
                                 },
                                 new Component
                                 {
                                     Type = "header",
                                      Parameters = new List<Parameter>
                                      {
                                          new Parameter
                                          {
                                               Type = "image",
                                                Image = new Image
                                                {
                                                     Link =pictureUrl
                                                }
                                          }
                                      }
                                 },
                                 new Component
                                 {
                                      Type ="button",
                                       Index = 0,
                                        SubType = "url",
                                         Parameters= new List<Parameter>
                                         {
                                             new Parameter
                                             {
                                                  Type= "text",
                                                 Text="11"
                                             }
                                         }
                                 }
                            }
                    }
                };

                return whatsAppModel;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return default;
            }
        }
    
    }
}