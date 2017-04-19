using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Event.Exceptions
{
    public class UnKnownErrorException : EventErrorException
    {
        public UnKnownErrorException(string Id, string Code) : base(Id, Code)
        {
        }
    }
}
