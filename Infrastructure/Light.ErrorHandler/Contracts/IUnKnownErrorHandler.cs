using Light.Event;
using Light.Event.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.ErrorHandler.Contracts
{
    public interface IUnKnownErrorHandler : IErrorHandler<UnKnownErrorEvent, Exception>
    {
    }
}
