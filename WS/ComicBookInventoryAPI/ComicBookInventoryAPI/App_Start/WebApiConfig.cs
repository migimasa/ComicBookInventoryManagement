﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ComicBookInventory.Infrastructure;

namespace ComicBookInventoryAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.DependencyResolver = new NinjectDependencyResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
