using Base.Contracts;
using Base.Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Base;

public abstract class BaseRepository<TEntity, TDbContext>: BaseRepository<TEntity, Guid, TDbContext>
    where TEntity : class, IDomainEntityId
    where TDbContext : DbContext
{
    protected BaseRepository(TDbContext dbContext) : base(dbContext)
    {
    }
}

public abstract class BaseRepository<TEntity, TKey, TDbContext> : IBaseRepository<TEntity, TKey>
    where TEntity: class, IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
    where TDbContext : DbContext
{
    private readonly TDbContext _repositoryDbContext;
    protected readonly DbSet<TEntity> RepositoryDbSet;

    protected BaseRepository(TDbContext dbContext)
    {
        _repositoryDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        RepositoryDbSet = _repositoryDbContext.Set<TEntity>();
    }
    
    public virtual async Task<IEnumerable<TEntity>> All()
    {
        return await RepositoryDbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<TEntity?> Find(TKey id)
    {
        return await RepositoryDbSet.FindAsync(id);
    }

    public virtual async Task<TEntity?> Add(TEntity entity)
    {
        if (await Find(entity.Id) != null) return null;
        await RepositoryDbSet.AddAsync(entity);
        await _repositoryDbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<TEntity?> Update(TEntity entity)
    {
        var newEntity = RepositoryDbSet.Update(entity).Entity;
        await _repositoryDbContext.SaveChangesAsync();
        return newEntity;
    }

    public virtual async Task<TEntity?> UpdateById(TKey id)
    {
        var entityToUpdate = await Find(id);
        if (entityToUpdate == null) return null;
        var newEntity = await Update(entityToUpdate);
        await _repositoryDbContext.SaveChangesAsync();
        return newEntity;
    }

    public virtual async Task<TEntity?> Remove(TEntity entity)
    {
        var newEntity = RepositoryDbSet.Remove(entity).Entity;
        await _repositoryDbContext.SaveChangesAsync();
        return newEntity;
    }

    public virtual async Task<TEntity?> RemoveById(TKey id)
    {
        var entityToRemove = await Find(id);
        if (entityToRemove == null) return null;
        await Remove(entityToRemove);
        await _repositoryDbContext.SaveChangesAsync();
        return entityToRemove;
    }
}
