using Base.DTO;

namespace Public.DTO.v1._0.Identity;

public class RefreshTokenModel : DTOEntityId
{
    public string JWT { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}