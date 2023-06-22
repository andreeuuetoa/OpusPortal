using Base.BLL.Contracts;
using Base.Contracts;
using Base.Domain.Contracts;
using Base.DTO.Contracts;

namespace Base.BLL;

public abstract class BaseEntityService<TDALEntity, TBLLEntity, TRepository> : BaseEntityService<TDALEntity, TBLLEntity, TRepository, Guid>, IBaseEntityService<TBLLEntity>
    where TBLLEntity : class, IDTOEntityId
    where TDALEntity : class, IDomainEntityId
    where TRepository : IBaseRepository<TDALEntity>
{
    protected BaseEntityService(TRepository repository, IMapper<TDALEntity, TBLLEntity> mapper) : base(repository, mapper)
    {
    }
}

public abstract class BaseEntityService<TDALEntity, TBLLEntity, TRepository, TKey> : IBaseEntityService<TBLLEntity, TKey> 
    where TBLLEntity : class, IDTOEntityId<TKey>
    where TDALEntity : class, IDomainEntityId<TKey>
    where TRepository : IBaseRepository<TDALEntity, TKey>
    where TKey : struct, IEquatable<TKey>
{
    protected readonly TRepository Repository;
    protected readonly IMapper<TDALEntity, TBLLEntity> Mapper;

    protected BaseEntityService(TRepository repository, IMapper<TDALEntity, TBLLEntity> mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public virtual async Task<IEnumerable<TBLLEntity>> All()
    {
        return (await Repository.All()).Select(e => Mapper.Map(e));
    }

    public virtual async Task<TBLLEntity?> Find(TKey id)
    {
        return Mapper.Map(await Repository.Find(id));
    }

    public virtual async Task<TBLLEntity?> Add(TBLLEntity entity)
    {
        return Mapper.Map(await Repository.Add(Mapper.Map(entity)));
    }

    public virtual async Task<TBLLEntity?> Update(TBLLEntity entity)
    {
        return Mapper.Map(await Repository.Update(Mapper.Map(entity)));
    }

    public virtual async Task<TBLLEntity?> UpdateById(TKey id)
    {
        return Mapper.Map(await Repository.UpdateById(id));
    }

    public virtual async Task<TBLLEntity?> Remove(TBLLEntity entity)
    {
        return Mapper.Map(await Repository.Remove(Mapper.Map(entity)));
    }

    public virtual async Task<TBLLEntity?> RemoveById(TKey id)
    {
        return Mapper.Map(await Repository.RemoveById(id));
    }
}