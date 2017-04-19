using Light.Domain.Bus.EventHandler;
using Light.Logger;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Light.Domain.Bus.Interfaces;
using Light.Event;
using Light.Domain.Bus.EventBus;
using Light.ErrorHandler.Contracts;
using Light.Event.Exceptions;

namespace Light.ErrorHandler
{
    public class UnKnownErrorHandler : ErrorHandler<UnKnownErrorEvent, Exception>, IUnKnownErrorHandler
    {
        public UnKnownErrorHandler(ILoggerController<UnKnownErrorEvent> loggerController) : base(loggerController)
        {
        }

        public override void Handle(Exception ex)
        {
            if (ex != null)
            {
                //Trigger UnKnowErrorException event
                EventBus.Instance.Trigger(new UnKnownErrorEvent(ex.Message)
                {
                    ErrorStack = ex.ToString()
                });
            }
        }
    }
}
