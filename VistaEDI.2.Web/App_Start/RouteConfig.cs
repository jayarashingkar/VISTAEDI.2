using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VistaEDI._2.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: "Default",               
                url: "{controller}/{action}/{id}",
                   defaults: new { controller = "Parser", action = "Index", id = UrlParameter.Optional }
               //  defaults: new { controller = "Parser", action = "Parse", id = UrlParameter.Optional }
            );
        }
    }
}
