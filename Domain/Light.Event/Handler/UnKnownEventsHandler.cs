using Light.Domain.Bus.EventHandler;
using Light.Event.Contracts;
using Light.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Light.Event.Exceptions;

namespace Light.Event.Handler
{
    public class UnKnownEventsHandler : GenericEventHandler<UnKnownErrorEvent>, IUnKnownEventsHandler
    {
        private ILoggerController<UnKnownErrorEvent> _controller;
        public UnKnownEventsHandler(ILoggerController<UnKnownErrorEvent> controller)
        {
            _controller = controller;
        }

        public override void Handle(UnKnownErrorEvent @event)
        {
            //log
            _controller.Logger.LogCritical(@event.ToString());
            var ex = new UnKnownErrorException(@event.EventId.ToString(), @event.ErrCode);
            throw ex;
        }

        public override async Task HandleAsync(UnKnownErrorEvent @event)
        {
            //log
            await Task.Factory.StartNew(() => {
                //log
                _controller.Logger.LogCritical(@event.ToString());
                var ex = new UnKnownErrorException(@event.EventId.ToString(), @event.ErrCode);
                throw ex;
            });
        }
    }
}
