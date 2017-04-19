using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Event.Exceptions
{
    public class NegativeErrorException : EventErrorException
    {
        public NegativeErrorException(string Id, string Code) : base(Id, Code)
        {
        }
    }
}
