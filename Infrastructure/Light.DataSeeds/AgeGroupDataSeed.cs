using Light.Domain.Entities;
using EntityFramework.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Light.DataContracts.Seeds;

namespace Light.DataSeeds
{
    public class AgeGroupDataSeed : IAgeGroupSeedContract
    {
        public AgeGroup[] GetAll()
        {
            var ageGroups = new List<AgeGroup>();
            ageGroups.Add(new AgeGroup() { MinAge = null, MaxAge = 2, Description = "Toddler" });
            ageGroups.Add(new AgeGroup() { MinAge = 2, MaxAge = 14, Description = "Child" });
            ageGroups.Add(new AgeGroup() { MinAge = 14, MaxAge = 20, Description = "Teenager" });
            ageGroups.Add(new AgeGroup() { MinAge = 20, MaxAge = 25, Description = "Early twenties" });
            ageGroups.Add(new AgeGroup() { MinAge = 25, MaxAge = 30, Description = "Almost thirty" });
            ageGroups.Add(new AgeGroup() { MinAge = 30, MaxAge = 50, Description = "Very adult" });
            ageGroups.Add(new AgeGroup() { MinAge = 50, MaxAge = 70, Description = "Kinda old" });
            ageGroups.Add(new AgeGroup() { MinAge = 70, MaxAge = 99, Description = "Old" });
            ageGroups.Add(new AgeGroup() { MinAge = 99, MaxAge = 110, Description = "Very old" });
            ageGroups.Add(new AgeGroup() { MinAge = 110, MaxAge = 199, Description = "Crazy ancient" });
            ageGroups.Add(new AgeGroup() { MinAge = 199, MaxAge = 4999, Description = "Vampire" });
            ageGroups.Add(new AgeGroup() { MinAge = 4999, MaxAge = null, Description = "Kauri tree" });
            return ageGroups.ToArray();
        }
    }
}
