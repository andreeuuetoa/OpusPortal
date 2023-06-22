using Base.DTO.Contracts;

namespace Base.BLL.Contracts;

/// <summary>
/// Service connected to a model
/// </summary>
public interface IBaseEntityService<TEntity> : IBaseEntityService<TEntity, Guid>
    where TEntity : class, IDTOEntityId
{
    
}

public interface IBaseEntityService<TEntity, in TKey> : IBaseService
    where TEntity : class, IDTOEntityId<TKey>
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