using Light.Command.CommandValidaters;
using Light.Command.PersonCommand;
using Light.Domain.Bus.Interfaces;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Command.Contracts
{
    [Intercept(typeof(CommandPropertyValidator))]
    public interface IPersonCommandHandler : 
        ICommandHandler<CreateNewPersonCommand>,
        ICommandHandler<ModifyExistingPersonCommand>
    {
    }
}
