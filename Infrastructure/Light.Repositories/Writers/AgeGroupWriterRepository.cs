using Light.DataContracts.Repositories;
using Light.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Light.DataContracts;
using Light.DataContracts.DataBase;

namespace Light.Repositories.Writers
{
    public sealed class AgeGroupWriterRepository : WriterRepository<AgeGroup>, IAgeGroupWriterRepositoryContract
    {
        public AgeGroupWriterRepository(IAgeRangerWriterDbContextContract context) : base(context)
        {
        }
    }
}
