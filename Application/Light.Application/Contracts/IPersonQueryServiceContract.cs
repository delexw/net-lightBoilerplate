using Light.Application.Interfaces;
using Light.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Application.Contracts
{
    public interface IPersonQueryServiceContract : IApplicationQueryService<PersonAgeGroupDto>
    {
    }
}
