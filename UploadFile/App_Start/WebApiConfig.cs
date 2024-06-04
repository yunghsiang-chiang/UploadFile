using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace UploadFile
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // New code for cors
            config.EnableCors();

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
