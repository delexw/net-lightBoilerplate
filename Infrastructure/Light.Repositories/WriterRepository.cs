using Light.DataContracts;
using Light.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Repositories
{
    /// <summary>
    /// Basic Repository Base for writer
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class WriterRepository<TEntity> : Repository<TEntity>, 
        IEntityWriterContract<TEntity> where TEntity : Entity, new()
    {
        public WriterRepository(IDbContextContract context) : base(context)
        {
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }

        public virtual async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Create a new entity and return the new one
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Create(TEntity entity)
        {
            _set.Add(entity);
        }

        public virtual void Delete(int? Id)
        {
            if (Id == null)
            {
                _context.Database.ExecuteSqlCommand($"delete from {typeof(TEntity).Name}");
                return;
            }
            var delete = new TEntity() { Id = Id.Value };
            _context.Entry<TEntity>(_set.Attach(delete)).State = EntityState.Deleted;
        }

        public virtual void Update(TEntity entity, byte[] rowVersion = null)
        {
            var updatingEntity = _set.Attach(entity);
            //Concurrency
            if (rowVersion != null && rowVersion.Length > 0)
            {
                var e = new TEntity();
                _context.Entry<TEntity>(updatingEntity).OriginalValues[nameof(e.RowVersion)] = rowVersion;
            }
            _context.Entry<TEntity>(updatingEntity).State = EntityState.Modified;
        }
    }
}
