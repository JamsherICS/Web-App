using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace _1_MVC_Northwind
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
                name: "CustomersInGermany",
                url: "Code/CustomersInGermany",
                defaults: new { controller = "Customer", action = "CustomersInGermany" }
            );

            routes.MapRoute(
                name: "CustomerDetailsWithOrder",
                url: "Code/CustomerDetailsWithOrder",
                defaults: new { controller = "Customer", action = "CustomerDetailsWithOrder" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
