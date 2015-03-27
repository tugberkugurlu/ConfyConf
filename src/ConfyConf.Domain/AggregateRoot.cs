using System.Collections.Generic;
using ConfyConf.Domain.Events;

namespace ConfyConf.Domain
{
    public abstract class AggregateRoot
    {
        // TODO: No LoadsFromHistory behaviour is available here.

        private readonly List<IDomainEvent> _changes;

        protected AggregateRoot()
        {
            _changes = new List<IDomainEvent>();
        }

        public string Id { get; protected set; }
        public int Version { get; protected set; }

        public IEnumerable<IDomainEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        protected void RecordChange(IDomainEvent @event)
        {
            _changes.Add(@event);
        }
    }
}