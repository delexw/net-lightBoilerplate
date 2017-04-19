using Light.Domain.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Bus.EventHandler
{
    public abstract class ExceptionEvent : IEvent
    {
        public ExceptionEvent(string message)
        {
            ErrorMessage = message;
        }

        public Guid EventId
        {
            get;
        } = Guid.NewGuid();

        /// <summary>
        /// Error code would be used in web app
        /// </summary>
        public string ErrCode
        {
            get
            {
                return $"{this.GetType().Name}";
            }
        }

        public string ErrorMessage {
            get;
            set;
        }

        public string ErrorStack
        {
            get; set;
        }

        public override string ToString()
        {
            return $"{EventId}|{ErrCode}|{ErrorMessage}|{ErrorStack}";
        }
    }
}
