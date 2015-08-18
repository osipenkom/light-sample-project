using Ant.Cargo.Client.App_Start;
using Ant.Cargo.Client.Infrastructure;
using Ant.Cargo.Client.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ant.Cargo.Client
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MappersConfiguration.CreateMaps();

            var container = UnityConfig.GetConfiguredContainer();
            var dependencyResolver = new UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = dependencyResolver;

            // Replace controller configuration
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector),
                new HttpControllerSelector(GlobalConfiguration.Configuration));

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}