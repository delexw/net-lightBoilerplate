using Light.Domain.Bus.EventHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Event.PersonEvent
{
    public class PersonNotCreatedEvent : ExceptionEvent
    {
        public PersonNotCreatedEvent(string errorMessage) : base(errorMessage)
        {
        }
    }
}
