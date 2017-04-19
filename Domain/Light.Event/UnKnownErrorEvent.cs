using Light.Domain.Bus.EventHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Event
{
    public class UnKnownErrorEvent : ExceptionEvent
    {
        public UnKnownErrorEvent(string message) : base(message)
        {
        }
    }
}
