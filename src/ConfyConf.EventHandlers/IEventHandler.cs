using ConfyConf.Domain.Events;

namespace ConfyConf.EventHandlers
{
    public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        void Execute(TEvent @event);
    }
}