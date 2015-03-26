namespace ConfyConf.Domain
{
    public interface IDomainRepository<TEntity> where TEntity : AggregateRoot
    {
        TEntity GetById(string id);
        void Add(TEntity entity);
    }
}
