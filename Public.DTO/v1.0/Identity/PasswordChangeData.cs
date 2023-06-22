using Base.DTO;

namespace Public.DTO.v1._0.Identity;

public class PasswordChangeData : DTOEntityId
{
    public string Email { get; set; } = default!;
    public string CurrentPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}