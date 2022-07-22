using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.Plugin.Flutterwave
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.Flutterwave",
                 "Plugins/Flutterwave/{action}",
                 new { controller = "Flutterwave", action = "Configure" },
                 new[] { "SmartStore.Flutterwave.Controllers" }
            )
            .DataTokens["area"] = "SmartStore.Flutterwave";
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
