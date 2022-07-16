using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.Plugin.WhatsApp
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.WhatsApp",
                 "Plugins/WhatsApp/{action}",
                 new { controller = "WhatsApp", action = "Configure" },
                 new[] { "SmartStore.WhatsApp.Controllers" }
            )
            .DataTokens["area"] = "SmartStore.WhatsApp";
        }

        public int Priority
        {
            get
            {
                return 0;
            }
        }
    }
}
