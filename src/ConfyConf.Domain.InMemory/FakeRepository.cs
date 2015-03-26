using System.Collections.Concurrent;

namespace ConfyConf.Domain.InMemory
{
    public class FakeRepository<TEntity> : IDomainRepository<TEntity> where TEntity : AggregateRoot
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
