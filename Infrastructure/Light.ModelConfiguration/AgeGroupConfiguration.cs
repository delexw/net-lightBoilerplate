using Light.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.ModelConfiguration
{
    public class AgeGroupConfiguration : EntityTypeConfiguration<AgeGroup>
    {
        public AgeGroupConfiguration()
        {
            this.HasKey(e => e.Id);

            this.ToTable(nameof(AgeGroup));
        }
    }
}
