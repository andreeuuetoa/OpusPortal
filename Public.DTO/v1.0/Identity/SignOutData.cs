using Base.DTO;

namespace Public.DTO.v1._0.Identity;

public class SignOutData : DTOEntityId
{
    public string RefreshToken { get; set; } = default!;
}