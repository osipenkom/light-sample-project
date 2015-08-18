using Ant.Cargo.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ant.Cargo.Data.Repositories
{
    /// <summary>
    /// This class implement all the generic methods that can be used across all repositories.
    /// It can be used to query or update data.
    /// </summary>
    public abstract class AbstractRepository : IRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository" /> class.
        /// </summary>
        protected AbstractRepository(AbstractDataContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets a reference to the DataContext associated with this repository.
        /// </summary>
        protected AbstractDataContext Context { get; private set; }

        /// <summary>
        /// Returns a reference to EF's ObjectContext.
        /// </summary>
        private ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)Context).ObjectContext; }
        }

        /// <summary>
        /// Get an entity from the database based on a optional criteria (one or more conditions on the attributes of the entity or related entities).
        /// This method only builds an object query, it does not execute it against the database.
        /// </summary>
        protected IQueryable<TEntity> GetQuery<TEntity>(Expression<Func<TEntity, bool>> criteria = null)
            where TEntity : class
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (criteria != null)
            {
                query = query.Where(criteria);
            }

            return query;
        }

        /// <summary>
        /// Gets a single entity from the database based on the key(s).
        /// </summary>
        protected TEntity GetByKey<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            return Context.Set<TEntity>().Find(keyValues);
        }

        /// <summary>
        /// Gets a single entity from the database based on an optional criteria.
        /// If no entity if found a null is returned.
        /// </summary>
        protected TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> criteria)
            where TEntity : class
        {
            return GetQuery<TEntity>().SingleOrDefault(criteria);
        }

        /// <summary>
        /// Adds an entity to the to the database.
        /// </summary>
        protected void Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Updates an attached entity from another entity.
        /// This method can be used when there is already an entity attached
        /// to the current context and we want to update all the fields from this entity.
        /// The entity should be first queried to be part attached to the context.
        /// </summary>
        protected void Update<TEntity>(TEntity entity, TEntity fromEntity)
            where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            Context.Entry(entity).CurrentValues.SetValues(fromEntity);
        }

        /// <summary>
        /// Attaches the entity to the context and sets the modified flag
        /// so the entire entity will be saved to the database.
        /// </summary>
        protected void UpdateWithAttach<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (!IsEntityAttached(entity))
            {
                Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// Checks if a given entity is attached to the context.
        /// </summary>
        protected bool IsEntityAttached<TEntity>(TEntity entity)
            where TEntity : class
        {
            return Context.Set<TEntity>().Local.Any(e => e == entity);
        }

        /// <summary>
        /// Removes the entity from the database table.
        /// </summary>        
        protected void Remove<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            Context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Attaches an entity to the context.
        /// </summary>
        protected void Attach<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Context.Set<TEntity>().Attach(entity);
        }

        /// <summary>
        /// Detaches an entity to the context.
        /// </summary>
        protected void Detach<TEntity>(TEntity entity)
            where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            ObjectContext.Detach(entity);
        }
    }

}
