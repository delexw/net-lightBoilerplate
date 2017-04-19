using Light.Domain.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Bus.CommandHandler
{
    public abstract class CommnadAggregate : ICommand, IOperator
    {
        public Guid CommandId { get; } = Guid.NewGuid();

        public byte[] CommandVersion { get; set; }
        public abstract Operator Operator { get; }
    }
}
