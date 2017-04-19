using Light.DataContracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFramework.Extensions;
using Light.Domain.Entities;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;

namespace Light.Repositories
{
    /// <summary>
    /// Basic Repository Base for reader
    /// </summary>
    /// <typeparam name="TEntity">Domain Entity</typeparam>
    public abstract class ReaderRepository<TEntity> : Repository<TEntity>, IEntityReaderContract<TEntity> where TEntity: Entity, new()
    {
        public ReaderRepository(IDbContextContract context):base(context)
        {
            if (!_context.Database.Exists())
            {
                //Fix don't create new DataBase in SQLite code first 
                this.Query();
            }
        }

        public virtual async Task<TEntity> GetById(int Id)
        {
            return await this.Query(entity => entity.Id == Id).FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? pageIndex = default(int?), int? pageCount = default(int?),
            params string[] includeProperties)
        {
            var query = _set.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var include in includeProperties)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderBy(r => r.Id);
            }

            if (pageIndex != null && pageCount != null)
            {
                //PageIndex starts at 1;
                var skipAmount = (pageIndex.Value - 1) * pageCount.Value;
                query = query.Skip(() => (skipAmount))
                    .Take(() => pageCount.Value);
            }

            return query.AsNoTracking();
        }
    }
}
