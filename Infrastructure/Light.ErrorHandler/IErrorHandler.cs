using Light.Domain.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.ErrorHandler
{
    public interface IErrorHandler<TEvent, TException> 
        where TEvent : IEvent
        where TException : Exception
    {
        /// <summary>
        /// Deal with exceptions threw by server not application
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="log"></param>
        void Handle(TException ex);
        Type EventType { get; set; }
    }
}
