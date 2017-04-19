using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Bus.Interfaces
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        void Handle(TEvent @event);
        Task HandleAsync(TEvent @event);
        int OrderId { get; set; }
    }
}
