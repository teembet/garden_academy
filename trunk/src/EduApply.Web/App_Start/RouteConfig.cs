using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EduApply.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute("GetStateByCountryId",
               "applicationform/getstatebycountryid/",
               new { controller = "ApplicationForm", action = "GetStateByCountryId" },
               new[] { "EduApply.Controllers" });

            routes.MapRoute("GetLgaByStateId",
               "applicationform/getlgabystateid/",
               new { controller = "ApplicationForm", action = "GetLgaByStateId" },
               new[] { "EduApply.Controllers" });

            routes.MapRoute("GetCoursesByProgramId",
               "applicationform/getcoursesbyprogramid/",
               new { controller = "ApplicationForm", action = "GetCoursesByProgramId" },
               new[] { "EduApply.Controllers" });
            
        }
    }
}
