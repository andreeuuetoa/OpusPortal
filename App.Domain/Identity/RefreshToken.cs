using Base.Domain;

namespace Domain.Identity;

public class RefreshToken : BaseRefreshToken
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}