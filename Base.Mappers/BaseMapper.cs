namespace Base.Mappers;

public class BaseMapper<TSource, TDestination> : IMapper<TSource, TDestination>
{
    protected readonly AutoMapper.IMapper _mapper;
    
    public BaseMapper(AutoMapper.IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public virtual TSource? Map(TDestination? entity)
    {
        return _mapper.Map<TSource>(entity);
    }

    public virtual TDestination? Map(TSource? entity)
    {
        return _mapper.Map<TDestination>(entity);
    }
}