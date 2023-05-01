namespace DomainEntities.Base.Interfaces
{
    public interface IRelatedEntitiesLoader<T>
    {
        public IRelatedEntitiesLoader<T> Add(T entity);
        public IRelatedEntitiesLoader<T> AddRange(IEnumerable<T> entities);
    }
}
