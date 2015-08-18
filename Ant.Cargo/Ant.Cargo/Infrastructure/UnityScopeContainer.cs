using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Ant.Cargo.Client.Infrastructure
{
    /// <summary>
    /// This is the IoC child container used to instantiate the controllers and all their dependencies.
    /// </summary>
    public class UnityScopeContainer : IDependencyScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityScopeContainer" /> class.
        /// </summary>
        public UnityScopeContainer(IUnityContainer container)
        {
            Container = container;
        }

        /// <summary>
        /// Gets a reference to the IoC container.
        /// </summary>
        protected IUnityContainer Container { get; set; }

        /// <summary>
        /// Resolves a type reference from the IoC container.
        /// Used by web api framework to resolve controllers and filters.
        /// </summary>
        public object GetService(Type serviceType)
        {
            return Container.IsRegistered(serviceType) ? Container.Resolve(serviceType) : null;
        }

        /// <summary>
        /// Resolves multiple type references from the IoC container.
        /// Used by web api framework to resolve controllers and filters.
        /// </summary>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.ResolveAll(serviceType);
        }

        /// <summary>
        /// Implementation of the dispose pattern.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Implementation of the dispose pattern.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Container.Dispose();
            }
        }
    }
}