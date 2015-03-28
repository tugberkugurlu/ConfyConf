using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ConfyConf.Domain.MongoDB
{
    public class MongoContext
    {
        private static readonly IDictionary<string, string> AggregateNameToCollectionNameMappings = new Dictionary<string, string>(StringComparer.Ordinal)
        {
            { "User", "Users" }
        };

        private readonly MongoDatabase _database;

        public MongoContext(MongoDatabase database)
        {
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            _database = database;
        }

        public MongoCollection GetCollection(string aggregateName)
        {
            if (aggregateName == null)
            {
                throw new ArgumentNullException("aggregateName");
            }

            string collectionName;
            if (AggregateNameToCollectionNameMappings.TryGetValue(aggregateName, out collectionName) == false)
            {
                throw new InvalidOperationException(string.Format("The specified aggregate name is not mapped to a collection: {0}", aggregateName));
            }

            return _database.GetCollection(collectionName);
        }
    }
}