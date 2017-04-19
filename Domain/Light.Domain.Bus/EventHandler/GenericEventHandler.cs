using Light.Domain.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Bus.EventHandler
{
    public abstract class GenericEventHandler<TEvent> : IEventHandler<TEvent>, IDisposable where TEvent : IEvent
    {
        public GenericEventHandler()
        {
        }

        public int OrderId
        {
            get;
            set;
        }

        public virtual void Dispose()
        {
        }

        public abstract void Handle(TEvent @event);

        public abstract Task HandleAsync(TEvent @event);
    }
}
