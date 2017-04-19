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
    public class PersonWriterRepository : WriterRepository<Person>, IPersonWriterRepositoryContract
    {
        public PersonWriterRepository(IAgeRangerWriterDbContextContract context) : base(context)
        {
        }
    }
}
