using Light.Domain.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Event.Contracts
{
    public interface IUnKnownEventsHandler : IEventHandler<UnKnownErrorEvent>
    {
    }
}
