using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Logger
{
    public interface ILoggerController<out TCategory>
    {
         ILogger Logger { get; }
    }
}
