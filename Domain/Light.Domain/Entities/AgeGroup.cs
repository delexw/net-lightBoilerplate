using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Entities
{
    public class AgeGroup : Entity
    {
        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        public string Description { get; set; }
    }
}
    