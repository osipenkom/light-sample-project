using Ant.Cargo.Data.Contracts;
using Ant.Cargo.Data.Contracts.Repo;
using Ant.Cargo.Data.Repo;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Data
{
    public class ContainerConfiguration
    {
        public static void RegisterTypes<TLifetime>(IUnityContainer container)
           where TLifetime : LifetimeManager, new()
        {
            container.RegisterType<IConnectionConfiguration, ConnectionConfiguration>(
                new TLifetime(),
                new InjectionConstructor("CargoConnection"));

            container.RegisterType<CargoContext>(new TLifetime());
            container.RegisterType<ICargoContextManager, ReadyRoomsContextManager>(new TLifetime());

            container.RegisterType<ICargoRepository, CargoRepository>(new TLifetime());
        }
    }
}
