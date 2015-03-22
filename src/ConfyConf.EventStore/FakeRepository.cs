using System.Collections.Concurrent;
using ConfyConf.Domain;

namespace ConfyConf.EventStore
{
    public class FakeRepository<TEntity> : IDomainRepository<TEntity> where TEntity : class, IEntity
    {
        private static readonly ConcurrentDictionary<string, TEntity> Store = 
            new ConcurrentDictionary<string, TEntity>();

        public TEntity GetById(string id)
        {
            TEntity entity;
            Store.TryGetValue(id, out entity);

            return entity;
        }

        public void Add(TEntity entity)
        {
            Store.TryAdd(entity.Id, entity);
        }
    }
}
