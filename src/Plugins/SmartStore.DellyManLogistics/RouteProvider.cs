using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.Plugin.DellyManLogistics
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.DellyManLogistics",
                 "Plugins/DellyManLogistics/{action}",
                 new { controller = "DellyManLogistics", action = "Configure" },
                 new[] { "SmartStore.DellyManLogistics.Controllers" }
            )
            .DataTokens["area"] = "SmartStore.DellyManLogistics";
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
