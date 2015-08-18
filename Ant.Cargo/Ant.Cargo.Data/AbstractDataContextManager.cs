using Microsoft.Practices.Unity;
using Ant.Cargo.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Ant.Cargo.Data
{
    /// <summary>
    /// Abstract class that creates repositories and data contexts.
    /// </summary>
    public abstract class AbstractDataContextManager<TContext> : IDataContextManager
        where TContext : AbstractDataContext
    {
        /// <summary>
        /// Keeps a list of all created repositories.
        /// </summary>
        private List<IRepository> _repositories = new List<IRepository>();

        /// <summary>
        /// Keeps a list of all created contexts.
        /// </summary>
        private List<TContext> _contexts = new List<TContext>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextManager{TContext}" /> class.
        /// </summary>
        protected AbstractDataContextManager(IUnityContainer container)
        {
            Container = container;
        }


        /// <summary>
        /// Returns the IoC container.
        /// </summary>
        protected IUnityContainer Container { get; private set; }

        /// <summary>
        /// Creates a new data context and return a handler to this.
        /// The handler can be used to execute different operations on the context (create repository, enable logging,...).
        /// </summary>
        public ContextHandler GetNewContext()
        {
            var dataContext = CreateContext();
            _contexts.Add(dataContext);

            return new ContextHandler(dataContext.ContextId);
        }

        /// <summary>
        /// Enables lazy loading for the default context or a specific context if a handler is specified.
        /// </summary>
        public void EnableLazyLoading(ContextHandler handler = null)
        {
            SetLazyLoading(true, handler);
        }

        /// <summary>
        /// Disables lazy loading for the default context or a specific context if a handler is specified.
        /// </summary>
        public void DisableLazyLoading(ContextHandler handler = null)
        {
            SetLazyLoading(false, handler);
        }

        /// <summary>
        /// Creates a new repository by using the default context or by using a context specified by the handler.
        /// If multiple repository must be built using the same context then the same context handler must be sent.
        /// If a data context is not yet being tracked by the DataContextManager, one will be created.
        /// </summary>
        public TRepository CreateRepository<TRepository>(ContextHandler handler = null)
            where TRepository : IRepository
        {
            var dataContext = GetContext(handler);

            var repository = Container.Resolve<TRepository>(new ParameterOverride("context", dataContext));
            _repositories.Add(repository);

            return repository;
        }

        /// <summary>
        /// Saves all the changes across all the contexts.
        /// </summary>
        public void Save()
        {
            using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                foreach (var context in _contexts)
                {
                    context.SaveChanges();
                }

                transaction.Complete();
            }
        }

        /// <summary>
        /// This is part of the Dispose pattern.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// This is part of the Dispose pattern.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var context in _contexts)
                {
                    if (!context.IsDisposed)
                    {
                        context.Dispose();
                    }
                }

                _contexts = new List<TContext>();
                _repositories = new List<IRepository>();
            }
        }

        /// <summary>
        /// Enables or disables lazy loading for the default context or a context specified by the handler.
        /// </summary>
        private void SetLazyLoading(bool enabled, ContextHandler handler = null)
        {
            TContext dataContext = GetContext(handler);
            dataContext.LazyLoading = enabled;
        }

        /// <summary>
        /// Creates or return either the default context or a context specified by the handler.
        /// If no context if found for the specified handler the a DataContextNotFoundException is raised.
        /// </summary>
        private TContext GetContext(ContextHandler handler = null)
        {
            TContext dataContext;

            if (handler == null)
            {
                dataContext = _contexts.FirstOrDefault();

                if (dataContext == null)
                {
                    dataContext = CreateContext();
                    _contexts.Add(dataContext);
                }
            }
            else
            {
                dataContext = _contexts.FirstOrDefault(c => c.ContextId == handler.ContextId);
                if (dataContext == null)
                {
                    throw new DataContextNotFoundException();
                }
            }

            return dataContext;
        }

        /// <summary>
        /// Creates either a default context or any other context if no default is specified.
        /// </summary>
        private TContext CreateContext()
        {
            var context = Container.Resolve<TContext>();
            return context;
        }

    }
}
