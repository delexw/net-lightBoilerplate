using Light.DataContracts.DataBase;
using Light.Domain.Entities;
using Light.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.SQLite
{
    public class AgeRangerWriterDbContext : AgeRangerDbContextBase, IAgeRangerWriterDbContextContract
    {
        public AgeRangerWriterDbContext()
            : base("name=AgeRangerDBWriter")
        {
        }
    }
}
