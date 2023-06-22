namespace Base;

public interface IMapper<TSource, TDestination>
{
    TSource? Map(TDestination? entity);
    TDestination? Map(TSource? entity);
}