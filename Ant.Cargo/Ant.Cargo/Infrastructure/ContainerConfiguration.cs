using Ant.Cargo.Client.Controllers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ant.Cargo.Client.Infrastructure
{
    public class ContainerConfiguration
    {
        /// <summary>
        /// Registers all the controller types and action filters in the container.
        /// Calls for registration of all the facade types.
        /// Calls for registration of all the mapper types.
        /// </summary>
        public static void RegisterTypes<TLifetime>(IUnityContainer container)
            where TLifetime : LifetimeManager, new()
        {
            Services.ContainerConfiguration.RegisterTypes<TLifetime>(container);

            container.RegisterType<HomeController>(new TLifetime());
            container.RegisterType<AuthorizationController>(new TLifetime());
            container.RegisterType<VehicleController>(new TLifetime());
            container.RegisterType<DistrictController>(new TLifetime());
        }
    }
}