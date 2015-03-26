using System.Collections.Generic;
using ConfyConf.Domain.Events;

namespace ConfyConf.Domain
{
    public interface IEventStore
    {
        // TODO: No version is passed through.

        void SaveEvents(string aggregateId, IEnumerable<IDomainEvent> events);
        IEnumerable<IDomainEvent> GetEventsForAggregate(string aggregateId);
    }
}