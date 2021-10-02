using SmartStore.Web.Framework.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartStore.Plugin.Paystack
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("SmartStore.Paystack",
                 "Plugins/Paystack/{action}",
                 new { controller = "Paystack", action = "Configure" },
                 new[] { "SmartStore.Paystack.Controllers" }
            )
            .DataTokens["area"] = "SmartStore.Paystack";
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
