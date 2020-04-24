using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VerificationDocs.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // в качестве значения controller указано Styles, а не StylesController, которое является именем класса. 
            // Это часть схемы именования ASP.NET MVC, в соответствии с которой классы контроллеров всегда заканчиваются
            // словом Controller, и при ссылке на класс эта часть имени опускается.
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Styles", action = "List", id = UrlParameter.Optional } // вывод списка стилей текта. Отладка
                defaults: new { controller = "MainPage", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
