using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Bus.Interfaces
{
    public interface ICommand
    {

        /// <summary>
        /// Command Id
        /// </summary>
        Guid CommandId { get; }
    }
}
