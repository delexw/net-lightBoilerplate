using Light.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Dtos
{
    public class PersonAgeGroupDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int Id { get; set; }

        public byte[] RowVersion { get; set; }

        public AgeGroup Group { get; set; }
    }
}
