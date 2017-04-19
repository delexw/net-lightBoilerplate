using Light.Domain.Bus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Light.Domain.Bus.EventHandler;

namespace Light.Domain.Bus.EventBus
{
    /// <summary>
    /// Simple Event Bus
    /// </summary>
    public class EventBus
    {
        private static EventBus _eventBus;

        /// <summary>
        /// EventBus Instance
        /// </summary>
        public static EventBus Instance
        {
            get { return _eventBus ?? (_eventBus = new EventBus()); }
        }

        /// <summary>
        /// Event Handlers Mapper
        /// </summary>
        private static Dictionary<Type, List<object>> eventHandlers = new Dictionary<Type, List<object>>();

        #region Subscribe or Withdraw event

        private readonly object sync = new object();
        /// <summary>
        /// Subscribe event
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHandler"></param>
        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent
        {
            lock (sync)
            {
                var eventType = typeof(TEvent);
                if (eventHandlers.ContainsKey(eventType))
                {
                    var handlers = eventHandlers[eventType];
                    if (handlers != null)
                    {
                        if (!handlers.Exists(deh => eventHandlerEquals(deh, eventHandler))) 
                        { 
                            handlers.Add(eventHandler);
                        }
                    }
                    else
                    {
                        handlers = new List<object>();
                        handlers.Add(eventHandler);
                    }
                }
                else 
                { 
                    eventHandlers.Add(eventType, new List<object> { eventHandler });
                }
            }
        }

        /// <summary>
        /// Subscribe multiple event handlers who handle the event of type TEvent
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHandlers"></param>
        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                Subscribe<TEvent>(eventHandler);
        }

        /// <summary>
        /// Withdraw event
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subType"></param>
        public void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler)
            where TEvent : class, IEvent
        {
            lock (sync)
            {
                var eventType = typeof(TEvent);
                if (eventHandlers.ContainsKey(eventType))
                {
                    var handlers = eventHandlers[eventType];
                    if (handlers != null
                        && handlers.Exists(deh => eventHandlerEquals(deh, eventHandler)))
                    {
                        var handlerToRemove = handlers.First(deh => eventHandlerEquals(deh, eventHandler));
                        handlers.Remove(handlerToRemove);
                    }
                }
            }
        }

        /// <summary>
        /// Withdraw multiple events
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHandlers"></param>
        public void Unsubscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
          where TEvent : class, IEvent
        {
            foreach (var eventHandler in eventHandlers)
                Unsubscribe<TEvent>(eventHandler);
        }

        #endregion

        #region Trigger event

        /// <summary>
        /// Trigger event
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="evnt"></param>
        public void Trigger<TEvent>(TEvent evnt)
            where TEvent : class, IEvent
        {
            if (evnt == null)
                throw new ArgumentNullException(nameof(evnt));
            var eventType = evnt.GetType();
            if (eventHandlers.ContainsKey(eventType)
                && eventHandlers[eventType] != null
                && eventHandlers[eventType].Count > 0)
            {
                var handlers = eventHandlers[eventType];
                foreach (var handler in handlers)
                {
                    var eventHandler = handler as IEventHandler<TEvent>;
                    eventHandler.OrderId = handlers.IndexOf(handler) + 1;
                    eventHandler.Handle(evnt);
                }
            }
        }

        /// <summary>
        /// Trigger event asynchronously
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="evnt"></param>
        public async Task TriggerAsync<TEvent>(TEvent evnt)
            where TEvent : class, IEvent
        {
            if (evnt == null)
                throw new ArgumentNullException(nameof(evnt));
            var eventType = evnt.GetType();
            if (eventHandlers.ContainsKey(eventType) &&
                eventHandlers[eventType] != null &&
                eventHandlers[eventType].Count > 0)
            {
                var handlers = eventHandlers[eventType];
                Task<Task> root = null;
                //Aggregate all async tasks
                foreach (var handler in handlers)
                {
                    var eventHandler = handler as IEventHandler<TEvent>;
                    eventHandler.OrderId = handlers.IndexOf(handler) + 1;
                    if (handlers.IndexOf(handler) == 0)
                    {
                        root = Task.FromResult(eventHandler.HandleAsync(evnt));
                    }
                    else
                    {
                        root = Task.FromResult(root.ContinueWith((t) =>
                        {
                            if (t.Exception == null)
                            {
                                eventHandler.HandleAsync(evnt);
                            }
                        }));
                    }
                }
                await root;
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// checks if the two event handlers are equal. if the event handler is an action-delegated, just simply
        /// compare the two with the object.Equals override (since it was overriden by comparing the two delegates. Otherwise,
        /// the type of the event handler will be used because we don't need to register the same type of the event handler
        /// more than once for each specific event.
        /// </summary>
        private readonly Func<object, object, bool> eventHandlerEquals = (o1, o2) =>
        {
            var o1Type = o1.GetType();
            var o2Type = o2.GetType();
            if (o1Type.IsGenericType &&
                o1Type.GetGenericTypeDefinition() == typeof(IEvent) &&
                o2Type.IsGenericType &&
                o2Type.GetGenericTypeDefinition() == typeof(IEvent)) 
            {
                return o1.Equals(o2);
            }
            return o1Type == o2Type;
        };

        #endregion

    }
}
