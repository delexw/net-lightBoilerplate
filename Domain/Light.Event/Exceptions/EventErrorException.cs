using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Event.Exceptions
{
    public abstract class EventErrorException : Exception
    {
        public EventErrorException(string Id, string Code)
        {
            Data["Id"] = Id;
            Data["Code"] = Code;
        }

        /// <summary>
        /// Return the info that exposes to external especially frontend
        /// </summary>
        /// <returns></returns>
        public virtual object GetExceptionInfo()
        {
            return new { Id = Data["Id"],
                Code = Data["Code"],
                Remark ="For more information, please contact system administrator" };
        }
    }
}
