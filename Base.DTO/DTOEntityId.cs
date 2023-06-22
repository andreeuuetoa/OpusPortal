using Base.DTO.Contracts;

namespace Base.DTO;

public class DTOEntityId : DTOEntityId<Guid>,  IDTOEntityId
{
    public override Guid Id { get; set; } = Guid.NewGuid();
}



public abstract class DTOEntityId<TKey> : IDTOEntityId<TKey>
    where TKey: struct, IEquatable<TKey>
{
    public virtual TKey Id { get; set; }
}