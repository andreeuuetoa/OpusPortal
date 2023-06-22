using Base.DTO;

namespace BLL.DTO.Identity;

public class AppUser : DTOEntityId
{
    public string Email { get; set; } = default!;
    
    public Guid AppRoleId { get; set; }
    public AppRole? AppRole { get; set; }

    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
}