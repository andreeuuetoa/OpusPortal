using System.ComponentModel.DataAnnotations;

namespace Base.Domain;

public abstract class BaseRefreshToken : BaseRefreshToken<Guid>
{
    
}

public abstract class BaseRefreshToken<TKey> : DomainEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
{
    [MaxLength(64)]
    public string RefreshToken { get; set; } = default!;

    public DateTime ExpirationDateTime { get; set; }
    [MaxLength(64)]
    public string? PreviousRefreshToken { get; set; }

    public DateTime? PreviousExpirationDateTime { get; set; }
}