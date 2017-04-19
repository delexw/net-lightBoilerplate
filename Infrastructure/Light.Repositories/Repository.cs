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
    /// RepositoryBase for reader and writer
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Repository<TEntity> where TEntity : Entity, new()
    {
        internal DbContext _context;
        internal DbSet<TEntity> _set;
        public Repository(IDbContextContract context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            _context = context as DbContext;
            _set = _context.Set<TEntity>();
        }
    }
}
