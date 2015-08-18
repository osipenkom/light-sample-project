using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Data.Contracts
{
    public interface IDataContextManager
    {
        /// <summary>
        /// Creates a new data context and returns a handler to it.
        /// This handler can then be used to execute other actions or to create repositories on this context.
        /// </summary>
        ContextHandler GetNewContext();

        /// <summary>
        /// Enables lazy loading for the default context or a specified context.
        /// If enabled these entities can be loaded on demand directly from the business model.
        /// </summary>
        void EnableLazyLoading(ContextHandler handler = null);

        /// <summary>
        /// Disables lazy loading for the default context or a specified context.
        /// </summary>
        void DisableLazyLoading(ContextHandler handler = null);

        /// <summary>
        /// Factory method to create repositories.
        /// All the repositories will share the same context specified by the 'handler' if one is passed as a parameter.
        /// If no 'handler' is specified all the repositories will share the same 'default' context.
        /// If a 'handler' is specified then this repository will be created in the specified context.
        /// </summary>
        TRepository CreateRepository<TRepository>(ContextHandler handler = null)
            where TRepository : IRepository;

        /// <summary>
        /// Saves all the data modified in all the contexts tracked by the current IDataContextManager.
        /// </summary>
        void Save();

    }
}
