using Light.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Light.Command.PersonCommand;
using Light.Command.Contracts;
using Light.Application.Interfaces;

namespace Light.Application.CommandServices
{
    public class PersonCommandService : IPersonCommandServiceContract
    {
        IPersonCommandHandler _handler;
        public PersonCommandService(IPersonCommandHandler handler)
        {
            _handler = handler;
        }


        public void Apply(CreateNewPersonCommand command)
        {
            _handler.Handle(command);
        }

        public async Task ApplyAsync(CreateNewPersonCommand command)
        {
            await _handler.HandleAsync(command);
        }

        public void Apply(ModifyExistingPersonCommand command)
        {
            _handler.Handle(command);
        }

        public async Task ApplyAsync(ModifyExistingPersonCommand command)
        {
            await _handler.HandleAsync(command);
        }
    }
}
