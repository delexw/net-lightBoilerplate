using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Entities
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }

    }
}
