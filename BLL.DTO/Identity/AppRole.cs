using Base.DTO;

namespace BLL.DTO.Identity;

public class AppRole : DTOEntityId
{
    public string Name { get; set; } = default!;
}