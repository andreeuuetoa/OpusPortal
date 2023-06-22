using Base.DTO;

namespace Public.DTO.v1._0.Identity;

public class AppRole : DTOEntityId
{
    public string Name { get; set; } = default!;
}