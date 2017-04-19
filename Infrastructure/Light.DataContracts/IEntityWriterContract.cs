using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.DataContracts
{
    public interface IEntityWriterContract<TEntity>
    {
        void Delete(int? Id);
        void Create(TEntity entity);
        void Update(TEntity entity, byte[] rowVersion = null);
        void Commit();
        Task CommitAsync();
    }
}
