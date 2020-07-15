using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

using static System.Linq.Expressions.Expression;

namespace FilesStorage.DAL.EF
{
    public abstract class BaseRepository
    {
        protected readonly DbContext _context;
        protected readonly Action<Exception> _commandFailureHandler;

        protected BaseRepository(DbContext context, Action<Exception> commandFailure)
        {
            _context = context;
            _commandFailureHandler = commandFailure;
        }

        #region HELPERS

        protected void Command(Action<DbContext> action, [Optional] Action<Exception> commandFailure)
        {
            try
            {
                action.Invoke(_context);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                var handler = (commandFailure == null) ? _commandFailureHandler : commandFailure;
                handler?.Invoke(exception);
            }
        }

        protected DbQuery<TEntity> Query<TEntity>(bool AsNoTracking = true)
            where TEntity : class
        {
            if (AsNoTracking)
                return _context.Set<TEntity>().AsNoTracking();
            else
                return _context.Set<TEntity>();
        }

        protected TEntity Attach<TEntity>(TEntity entity)
            where TEntity : class
        {
            var attachedEntity = (_context.Entry(entity).State != EntityState.Detached) ? 
                entity : _context.Set<TEntity>().Attach(entity);
            return attachedEntity;
        }

        #endregion

        #region COMMANDS

        protected  void AddEntity<TEntity>(TEntity entity)
            where TEntity : class
        {
            Command(c => c.Set<TEntity>().Add(entity));
        }

        protected  void DeleteEntity<TEntity, TKey>(TKey id)
             where TEntity : class
        {
            Command(c => 
            {
                var entity = FindEntityById<TEntity, TKey>(id, false);
                _context.Entry(entity).State = EntityState.Deleted;
            });
        }

        protected  void UpdateEntity<TEntity>(TEntity entity)
             where TEntity : class
        {
            Command(c => 
            {
                c.Entry(entity).State = EntityState.Modified;
            });
        }

        protected void CustomEntityUpdate<TEntity, TKey>(TEntity editedEntity, TKey id, 
            [Optional] params Expression<Func<TEntity, object>>[] propsToUpdate)
            where TEntity : class
        {
            Command(c =>
            {
                var attachedEntity = FindEntityById<TEntity, TKey>(id, false);
                var entityEntry = c.Entry(attachedEntity);
                if(propsToUpdate == null || !propsToUpdate.Any())
                {
                    UpdateEntity(editedEntity);
                }
                else
                {
                    foreach (var prop in propsToUpdate)
                    {
                        if (prop != null)
                        {
                            var propEntry = entityEntry.Property(prop);
                            var newValue = typeof(TEntity).GetProperty(propEntry.Name).GetValue(editedEntity);
                            propEntry.CurrentValue = newValue;
                            propEntry.IsModified = true;
                        }
                    }                        
                }                
            });
        }

        protected  void AddRange<TEntity>(params TEntity[] entities)
             where TEntity : class
        {            
            Command(c => c.Set<TEntity>().AddRange(entities));
        }

        protected  void DeleteRange<TEntity>(params TEntity[] entities)
             where TEntity : class
        {
            Command(c => entities.Select(e => c.Entry(e).State = EntityState.Deleted));
        }

        #endregion

        #region QUERIES
        
        protected  TEntity FindEntityById<TEntity, TKey>(TKey id, bool AsNoTracking = true)
            where TEntity : class
        {
            var refEntity = Parameter(typeof(TEntity), $"{typeof(TEntity).Name}");
            var condition = Equal(Property(refEntity, $"Id"), Constant(id));
            var filter = Lambda<Func<TEntity, bool>>(condition, refEntity);
            return Query<TEntity>(AsNoTracking).FirstOrDefault(filter);
        }

        protected  IEnumerable<TEntity> GetAllWithEagerLoad<TEntity>
            ([Optional] params Expression<Func<TEntity, object>>[] loadProperties)
            where TEntity : class
        {
            if (loadProperties == null || !loadProperties.Any())
            {
                return Query<TEntity>().ToList();
            }
            else
            {
                return LoadWithProperties(loadProperties).ToList();
            }
        }

        protected  IEnumerable<TEntity> FindByWithEagerLoad<TEntity>(Expression<Func<TEntity, bool>> filter,
           [Optional] params Expression<Func<TEntity, object>>[] loadProperties)
            where TEntity : class
        {
            if (loadProperties == null || !loadProperties.Any())
            {
                return Query<TEntity>().Where(filter).ToList();
            }
            else
            {
                return LoadWithProperties(loadProperties).Where(filter).ToList();
            }
        }

        protected IQueryable<TEntity> LoadWithProperties<TEntity>(params Expression<Func<TEntity, object>>[] loadProperties)
            where TEntity : class
        {
            IQueryable<TEntity> querable = Query<TEntity>();
            return loadProperties.Aggregate
                (querable, (current, includeProperty) => current.Include(includeProperty));
        }

        #endregion

    }
}
