using ConfyConf.Domain.Events;

namespace ConfyConf.Bus
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}