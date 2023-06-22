namespace Base.DTO.Contracts;

public interface IDTOEntityId : IDTOEntityId<Guid>
{
}

public interface IDTOEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
{
    TKey Id { get; set; }
}