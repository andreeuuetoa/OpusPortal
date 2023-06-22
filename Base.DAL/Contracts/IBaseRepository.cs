using Base.Domain.Contracts;

namespace Base.Contracts;

public interface IBaseRepository<TEntity> : IBaseRepository<TEntity, Guid> 
    where TEntity : class, IDomainEntityId
{
    
}

public interface IBaseRepository<TEntity, in TKey> 
    where TEntity : class, IDomainEntityId<TKey> 
    where TKey : struct, IEquatable<TKey>
{
    Task<IEnumerable<TEntity>> All();

    Task<TEntity?> Find(TKey id);
    
    Task<TEntity?> Add(TEntity entity);

    Task<TEntity?> Update(TEntity entity);
    Task<TEntity?> UpdateById(TKey id);
    Task<TEntity?> Remove(TEntity entity);
    Task<TEntity?> RemoveById(TKey id);
}