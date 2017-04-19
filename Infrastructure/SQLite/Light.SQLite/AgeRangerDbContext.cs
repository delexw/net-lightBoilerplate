using Light.DataContracts.DataBase;
using Light.Domain.Entities;
using Light.ModelConfiguration;
using EntityFramework.Toolkit;
using EntityFramework.Toolkit.Core;
using EntityFramework.Toolkit.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.SQLite
{
    /// <summary>
    /// Database context under EntityFramework.Toolkit
    /// </summary>
    public class AgeRangerDbContext : AgeRangerDbContextBase, IAgeRangerReaderDbContextContract
    {
        public AgeRangerDbContext()
            : base("name=AgeRangerDB")
        {
        }
    }
}
