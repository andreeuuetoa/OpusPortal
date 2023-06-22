using System.ComponentModel.DataAnnotations;
using Base.DTO;

namespace Public.DTO.v1._0.Identity;

public class RegistrationData : DTOEntityId
{
    [StringLength(128, MinimumLength = 5, ErrorMessage = "Incorrect length")]
    public string Email { get; set; } = default!;
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string Password { get; set; } = default!;
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string FirstName { get; set; } = default!;
    [StringLength(128, MinimumLength = 1, ErrorMessage = "Incorrect length")]
    public string LastName { get; set; } = default!;

    public string? AppRoleName { get; set; }
}