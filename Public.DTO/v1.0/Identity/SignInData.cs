using System.ComponentModel.DataAnnotations;
using Base.DTO;

namespace Public.DTO.v1._0.Identity;

public class SignInData : DTOEntityId
{
    [MinLength(5, ErrorMessage = "Email is too short!")]
    [MaxLength(128, ErrorMessage = "Email is too long!")]
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}