﻿using SmartStore.Core;
using SmartStore.Core.Events;
using SmartStore.Web.Framework.Events;
using SmartStore.Web.Framework.Modelling;
using System.Web.Mvc.Html;

namespace SmartStore.WhatsApp
{
    public class TabStripCreatedEventConsumer : IConsumer
    {
        private readonly IStoreContext _storeContext;

        public TabStripCreatedEventConsumer(IStoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        /// <summary>
        /// You can inject own tabs into every tab strip in the backend. 
        /// Show below are examples for the most common tabs to inject content.
        /// </summary>
        public void HandleEvent(TabStripCreated eventMessage)
        {
            var tabStripName = eventMessage.TabStripName;
            var entityId = ((EntityModelBase)eventMessage.Model).Id;
            var entityName = eventMessage.TabStripName.Substring(0, eventMessage.TabStripName.IndexOf("-"));

            if (tabStripName == "category-edit" || tabStripName == "product-edit")
            {
                eventMessage.ItemFactory.Add().Text("WhatsApp")
                    .Name("tab-WhatsApp")
                    .Icon("fa fa-picture-o fa-lg fa-fw")
                    .LinkHtmlAttributes(new { data_tab_name = "WhatsApp" })
                    .Route("SmartStore.WhatsApp", new { action = "AdminEditTab", entityId, entityName })
                    .Ajax();
            }

            // Topic tab is no ajax tab, therefore it needs to inject content directly.
            if (tabStripName == "topic-edit")
            {
                eventMessage.ItemFactory.Add().Text("WhatsApp")
                    .Name("tab-WhatsApp")
                    .Icon("fa fa-edit fa-lg fa-fw")
                    .LinkHtmlAttributes(new { data_tab_name = "WhatsApp" })
                    .Content(eventMessage.Html.Action("AdminEditTab", "WhatsApp", new { action = "AdminEditTab", entityId, entityName, area = "SmartStore.WhatsApp" }).ToHtmlString())
                    .Route("SmartStore.WhatsApp", new { action = "AdminEditTab", entityId, entityName });
            }
        }
    }
}