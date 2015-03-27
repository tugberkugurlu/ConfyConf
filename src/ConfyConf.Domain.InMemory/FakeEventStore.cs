using System;
using System.Collections.Generic;
using ConfyConf.Bus;
using ConfyConf.Domain.Events;

namespace ConfyConf.Domain.InMemory
{
    public class FakeEventStore : IEventStore
    {
        private readonly Dictionary<string, List<EventDescriptor>> _current;
        private readonly IEventPublisher _publisher;

        public FakeEventStore(IEventPublisher publisher)
        {
            if (publisher == null)
            {
                throw new ArgumentNullException("publisher");
            }

            _current = new Dictionary<string, List<EventDescriptor>>();
            _publisher = publisher;
        }

        public void SaveEvents(string aggregateId, IEnumerable<IDomainEvent> events)
        {
            List<EventDescriptor> eventDescriptors;

            // try to get event descriptors list for given aggregate id, otherwise -> create empty dictionary
            if (!_current.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                _current.Add(aggregateId, eventDescriptors);
            }

            // iterate through current aggregate events increasing version with each processed event
            foreach (IDomainEvent @event in events)
            {
                // push event to the event descriptors list for current aggregate
                eventDescriptors.Add(new EventDescriptor(aggregateId, @event));

                // publish current event to the bus for further processing by subscribers
                _publisher.Publish(@event);
            }
        }

        public IEnumerable<IDomainEvent> GetEventsForAggregate(string aggregateId)
        {
            throw new NotImplementedException();
        }

        private struct EventDescriptor
        {
            private readonly string _id;
            private readonly IDomainEvent _eventData;

            public EventDescriptor(string id, IDomainEvent eventData)
            {
                if (id == null)
                {
                    throw new ArgumentNullException("id");
                }

                if (eventData == null)
                {
                    throw new ArgumentNullException("eventData");
                }

                _id = id;
                _eventData = eventData;
            }

            public string Id
            {
                get { return _id; }
            }

            public IDomainEvent EventData
            {
                get { return _eventData; }
            }
        }
    }
}
