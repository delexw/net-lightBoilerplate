using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Light.DataContracts
{
    public interface IEntityReaderContract<TEntity>
    {
        /// <summary>
        /// Get entities
        /// </summary>
        /// <param name="filter">filter expression</param>
        /// <param name="orderBy">order rules</param>
        /// <param name="pageIndex">index of data page</param>
        /// <param name="pageCount">quantity of records of each data page</param>
        /// <param name="includeProperties">included related entities</param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? pageIndex = null,
            int? pageCount = null,
            params string[] includeProperties);
        Task<TEntity> GetById(int Id);
    }
}
