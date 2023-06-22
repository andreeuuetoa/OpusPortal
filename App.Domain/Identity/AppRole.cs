using System.ComponentModel.DataAnnotations;
using Base.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity;

public class AppRole : IdentityRole<Guid>, IDomainEntityId
{
    [MinLength(1)]
    [MaxLength(256)]
    public override string Name { get; set; } = default!;
    
    public ICollection<AppUser>? AppUsers { get; set; }
}