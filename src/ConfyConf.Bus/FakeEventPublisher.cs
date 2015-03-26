using System;
using System.Collections.Generic;
using System.Linq;
using ConfyConf.Domain.Events;
using ConfyConf.EventHandlers;

namespace ConfyConf.Bus
{
    public class FakeEventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _lifetimeScope;

        public FakeEventPublisher(IServiceProvider lifetimeScope)
        {
            if (lifetimeScope == null)
            {
                throw new ArgumentNullException("lifetimeScope");
            }

            _lifetimeScope = lifetimeScope;
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            IEnumerable<IEventHandler<TEvent>> handlers = _lifetimeScope.GetService<IEnumerable<IEventHandler<TEvent>>>();
            var eventHandlers = handlers as IEventHandler<TEvent>[] ?? handlers.ToArray();
            if (eventHandlers.Any())
            {
                foreach (IEventHandler<TEvent> eventHandler in eventHandlers)
                {
                    eventHandler.Execute(@event);
                }
            }
            else
            {
                throw new InvalidOperationException("No handler registered");
            }
        }
    }
}
