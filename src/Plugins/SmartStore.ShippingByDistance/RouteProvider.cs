using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.Plugin.ShippingByDistance
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.ShippingByDistance",
                 "Plugins/ShippingByDistance/{action}",
                 new { controller = "ShippingByDistance", action = "Configure" },
                 new[] { "SmartStore.ShippingByDistance.Controllers" }
            )
            .DataTokens["area"] = "SmartStore.ShippingByDistance";
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
