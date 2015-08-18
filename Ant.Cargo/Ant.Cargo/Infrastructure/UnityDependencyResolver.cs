using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http.Dependencies;

namespace Ant.Cargo.Client.Infrastructure
{
    /// <summary>
    /// Replaces the default web api dependency resolver with a custom one in order to allow injecting dependencies into controllers.
    /// Each request to a controller starts into a new child container so HierarchicalLifetimeManager creates objects in the child container.
    /// This isolates the request between each other.
    /// </summary>
    public class UnityDependencyResolver : UnityScopeContainer, System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnityDependencyResolver" /> class.
        /// </summary>
        public UnityDependencyResolver(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Starts a new dependency scope by creating a child IoC container.
        /// </summary>
        public IDependencyScope BeginScope()
        {
            var childContainer = Container.CreateChildContainer();

            return new UnityScopeContainer(childContainer);
        }

        /// <summary>
        /// Implementation of the dispose pattern.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Dispose();
            }
        }
    }
}