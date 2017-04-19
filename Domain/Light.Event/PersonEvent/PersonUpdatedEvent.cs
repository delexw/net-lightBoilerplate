using Light.Domain.Bus;
using Light.Domain.Bus.EventHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Event.PersonEvent
{
    public sealed class PersonUpdatedEvent : VersionedEvent
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public override Operator Operator
        {
            get;
        } = Operator.Update;
    }
}
