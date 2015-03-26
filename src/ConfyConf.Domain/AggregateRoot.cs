using System.Collections.Generic;
using ConfyConf.Domain.Events;

namespace ConfyConf.Domain
{
    public abstract class AggregateRoot
    {
        protected AggregateRoot()
        {
            Changes = new List<IDomainEvent>();
        }

        public string Id { get; protected set; }
        public int Version { get; protected set; }

        protected ICollection<IDomainEvent> Changes { get; private set; }
    }
}