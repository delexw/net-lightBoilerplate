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
    public sealed class PersonReaderRepository : ReaderRepository<Person>, IPersonReaderRepositoryContract
    {
        public PersonReaderRepository(IAgeRangerReaderDbContextContract context) : base(context) { }
    }
}
