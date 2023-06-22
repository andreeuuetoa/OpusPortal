namespace Base.DTO;

public class BaseRefreshToken : BaseRefreshToken<Guid>
{
    
}

public abstract class BaseRefreshToken<TKey> : DTOEntityId<TKey>
    where TKey : struct, IEquatable<TKey>
{
    public string RefreshToken { get; set; } = default!;

    public DateTime ExpirationDateTime { get; set; }
    public string? PreviousRefreshToken { get; set; }

    public DateTime? PreviousExpirationDateTime { get; set; }
}