using Light.DataContracts.DataBase;
using Light.DataContracts.Repositories;
using Light.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Repositories.Readers
{
    public sealed class AgeGroupReaderRepository : ReaderRepository<AgeGroup>, IAgeGroupReaderRepositoryContract
    {
        public AgeGroupReaderRepository(IAgeRangerReaderDbContextContract context) : base(context) { }
    }
}
