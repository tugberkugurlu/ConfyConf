﻿using System;
using System.Collections.Generic;
using System.Linq;
using ConfyConf.Bus;
using ConfyConf.Domain.Events;
using MongoDB.Driver;

namespace ConfyConf.Domain.MongoDB
{
    public class MongoContext
    {
        public MongoCollection GetCollection(string aggregateName)
        {
            throw new NotImplementedException();
        }
    }

    public class MongoEventStore : IEventStore
    {
        // TODO: Move dubplicate code into a base class.

        private readonly MongoContext _context;
        private readonly IEventPublisher _publisher;

        public MongoEventStore(MongoContext context, IEventPublisher publisher)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (publisher == null)
            {
                throw new ArgumentNullException("publisher");
            }

            _context = context;
            _publisher = publisher;
        }

        public void SaveEvents(string aggregateId, IEnumerable<IDomainEvent> events)
        {
            if (aggregateId == null)
            {
                throw new ArgumentNullException("aggregateId");
            }

            if (events == null)
            {
                throw new ArgumentNullException("events");
            }

            IDomainEvent[] domainEvents = events as IDomainEvent[] ?? events.ToArray();
            MongoUpdateContext mongoUpdate = GenerateUpdate(domainEvents);
            IMongoQuery monogoUpdateQuery = GenerateUpdateQuery(aggregateId);
            ExecuteUpdate(monogoUpdateQuery, mongoUpdate);
            PublishImpl(domainEvents);
        }

        private void PublishImpl(IEnumerable<IDomainEvent> events)
        {
            // iterate through current aggregate events increasing version with each processed event
            foreach (IDomainEvent @event in events)
            {
                // publish current event to the bus for further processing by subscribers
                _publisher.Publish(@event);
            }
        }

        private IMongoQuery GenerateUpdateQuery(string aggregateId)
        {
            throw new NotImplementedException();
        }

        private MongoUpdateContext GenerateUpdate(IEnumerable<IDomainEvent> events)
        {
            throw new NotImplementedException();
        }

        private void ExecuteUpdate(IMongoQuery mongoUpdateQuery, MongoUpdateContext mongoUpdate)
        {
            if (mongoUpdate == null)
            {
                throw new ArgumentNullException("mongoUpdate");
            }

            MongoCollection collection = _context.GetCollection(mongoUpdate.AggregateName);

            try
            {
                collection.Update(mongoUpdateQuery, mongoUpdate.Update);
            }
            catch
            {
                // TODO: Handle aggregate not found
                // TODO: Handle concurrency

                throw;
            }
        }

        public IEnumerable<IDomainEvent> GetEventsForAggregate(string aggregateId)
        {
            throw new NotImplementedException();
        }

        private class MongoUpdateContext
        {
            private readonly string _aggregateName;
            private readonly IMongoUpdate _update;

            public MongoUpdateContext(string aggregateName, IMongoUpdate update)
            {
                if (aggregateName == null)
                {
                    throw new ArgumentNullException("aggregateName");
                }

                if (update == null)
                {
                    throw new ArgumentNullException("update");
                }

                _aggregateName = aggregateName;
                _update = update;
            }

            public string AggregateName
            {
                get { return _aggregateName; }
            }

            public IMongoUpdate Update
            {
                get { return _update; }
            }
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