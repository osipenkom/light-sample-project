using Microsoft.Practices.Unity;
using Ant.Cargo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ant.Cargo.Services.Contracts;
using Ant.Cargo.Services.Mappers.Contracts;
using Ant.Cargo.Services.Mappers;

namespace Ant.Cargo.Services
{
    public class ContainerConfiguration
    {
        public static void RegisterTypes<TLifetime>(IUnityContainer container)
          where TLifetime : LifetimeManager, new()
        {
            Data.ContainerConfiguration.RegisterTypes<TLifetime>(container);
            MappersConfiguration.CreateMaps();

            RegisterMappers<TLifetime>(container);
            RegisterServices<TLifetime>(container);
        }

        private static void RegisterMappers<TLifetime>(IUnityContainer container)
            where TLifetime : LifetimeManager, new()
        {
            container.RegisterType<IDistrictMapper, DistrictMapper>(new TLifetime());
            container.RegisterType<IVehicleMapper, VehicleMapper>(new TLifetime());
            container.RegisterType<IUserMapper, UserMapper>(new TLifetime());
        }

        private static void RegisterServices<TLifetime>(IUnityContainer container)
            where TLifetime : LifetimeManager, new()
        {
            container.RegisterType<ICargoService, CargoService>(new TLifetime());
        }

    }
}
