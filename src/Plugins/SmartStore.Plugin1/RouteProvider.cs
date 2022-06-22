using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.Plugin.Plugin1
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.Plugin1",
                 "Plugins/Plugin1/{action}",
                 new { controller = "Plugin1", action = "Configure" },
                 new[] { "SmartStore.Plugin1.Controllers" }
            )
            .DataTokens["area"] = "SmartStore.Plugin1";
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
