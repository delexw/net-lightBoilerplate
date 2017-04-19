using Light.Domain.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Domain.Bus.EventHandler
{
    public abstract class VersionedEvent : IEvent, IOperator
    {
        public Guid EventId
        {
            get;
        } = Guid.NewGuid();

        public byte[] EventVersion { get; set; }

        public abstract Operator Operator { get; }

        public override string ToString()
        {
            return $"{EventId}|{Encoding.UTF8.GetString(EventVersion)}";
        }
    }
}
