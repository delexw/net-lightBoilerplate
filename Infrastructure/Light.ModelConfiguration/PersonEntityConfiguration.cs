using Light.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.ModelConfiguration
{
    public class PersonEntityConfiguration<TPerson> : EntityTypeConfiguration<TPerson> where TPerson : Person
    {
        public PersonEntityConfiguration()
        {
            this.HasKey(e => e.Id);

            this.Property(e => e.RowVersion).IsConcurrencyToken();

            this.ToTable(nameof(Person));
        }
    }
}
