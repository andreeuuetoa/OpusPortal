using Base.DTO;

namespace Public.DTO.v1._0.Studying_logic;

public class Class : DTOEntityId
{
    public string Subject { get; set; } = default!;
    public string Room { get; set; } = default!;
    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}