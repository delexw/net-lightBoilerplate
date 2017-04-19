using Light.Domain.Bus.Interfaces;
using Light.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.ErrorHandler
{
    public abstract class ErrorHandler<TEvent, TException> : IErrorHandler<TEvent, TException> 
        where TEvent : IEvent
        where TException : Exception
    {
        private ILoggerController<TEvent> _loggerController;
        public ErrorHandler(ILoggerController<TEvent> loggerController) {
            _loggerController = loggerController;
        }

        public Type EventType
        {
            get; set;
        } = typeof(TEvent);

        public abstract void Handle(TException ex);
    }
}
