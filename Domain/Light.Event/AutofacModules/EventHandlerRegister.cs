using Light.Event.Contracts;
using Light.Event.Handler;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.Event.AutofacModules
{
    public class EventHandlerRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NegativeEventsHandler>()
                .As<INegativeEventsHandler>();

            builder.RegisterType<UnKnownEventsHandler>()
                .As<IUnKnownEventsHandler>();

            builder.RegisterType<AuditVersionedEventHandler>()
                .As<IAuditVersionedEventHandler>();
        }
    }
}
