namespace Base.Domain.Contracts;

public interface IDomainEntityId : IDomainEntityId<Guid>
{
}

public interface IDomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
{
    TKey Id { get; set; }
}