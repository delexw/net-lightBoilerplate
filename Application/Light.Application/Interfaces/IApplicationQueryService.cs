using Light.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Light.Application.Interfaces
{
    public interface IApplicationQueryService<TDto> : IApplicationService where TDto : class
    {
        /// <summary>
        /// Get DTO
        /// </summary>
        /// <param name="filter">filter expression</param>
        /// <param name="orderBy">order rules</param>
        /// <param name="pageIndex">index of data page</param>
        /// <param name="pageCount">quantity of records of each data page</param>
        /// <param name="includeProperties">included related entities</param>
        /// <returns></returns>
        Task<IEnumerable<TDto>> Query(
            string filter = null,
            string orderBy = null,
            int? pageIndex = null,
            int? pageCount = null,
            params string[] includeProperties);
        Task<TDto> GetById(int Id);
    }
}
