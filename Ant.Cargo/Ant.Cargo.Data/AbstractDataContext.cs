using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Ant.Cargo.Data
{
    /// <summary>
    /// This is a wrapper for EF <see cref="DbContext" /> class.
    /// Must be inherited in order to define all the entities tracked by this context.
    /// </summary>
    public abstract class AbstractDataContext : DbContext
    {
        /// <summary>
        /// Stores the generated contextId. This will be used to match a ContextHandler to its DataContext.
        /// </summary>
        private readonly string _contextId;


        /// <summary>
        /// This is true if the DataContext has been disposed.
        /// </summary>
        private bool _isDisposed;

        // <summary>
        /// Initializes a new instance of the <see cref="DataContext" /> class.
        /// </summary>
        protected AbstractDataContext(ConnectionConfiguration configuration)
            : base(configuration.GetConnectionString())
        {
            _contextId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Stores the generated contextId. This will be used to match a ContextHandler to its DataContext.
        /// </summary>
        public string ContextId
        {
            get { return _contextId; }
        }

        /// <summary>
        /// This is true if the DataContext has been disposed.
        /// </summary>
        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        /// <summary>
        /// This is the implementation of the dispose pattern.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _isDisposed = true;
        }

        /// <summary>
        /// If lazy loading is enabled entities can be loaded from the database directly in the domain model.
        /// </summary>
        public bool LazyLoading
        {
            get { return Configuration.LazyLoadingEnabled; }
            set { Configuration.LazyLoadingEnabled = value; }
        }

    }
}
