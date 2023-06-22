using Base.DTO;

namespace Public.DTO.v1._0.Identity;

public class AppUser : DTOEntityId
{
    public string Email { get; set; } = default!;
    
    public Guid AppRoleId { get; set; }
    public string RoleName { get; set; } = default!;

    public Guid? PersonId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}