using Light.Domain.Bus.EventHandler;
using Light.Event.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Light.Logger;
using Light.ErrorHandler.Contracts;

namespace Light.ErrorHandler
{
    public class NegativeErrorHandler : ErrorHandler<ExceptionEvent, NegativeErrorException>, INegativeErrorHandler
    {
        public NegativeErrorHandler(ILoggerController<ExceptionEvent> loggerController) : base(loggerController)
        {
        }

        public override void Handle(NegativeErrorException ex)
        {
            //Do something here if the error is a business rule error
        }
    }
}
