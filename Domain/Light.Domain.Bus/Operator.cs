using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Bus
{
    public enum Operator
    {
        /// <summary>
        /// Create
        /// </summary>
        Add,
        /// <summary>
        /// Update
        /// </summary>
        Update,
        /// <summary>
        /// Delete
        /// </summary>
        Delete
    }
}
