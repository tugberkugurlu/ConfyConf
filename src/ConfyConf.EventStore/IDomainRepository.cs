using ConfyConf.Domain;

namespace ConfyConf.EventStore
{
    public interface IDomainRepository<TEntity> where TEntity : IEntity
    {
        TEntity GetById(string id);
        void Add(TEntity entity);
    }
}
