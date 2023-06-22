using Base.DTO;

namespace Public.DTO.v1._0.Studying_logic;

public class Room : DTOEntityId
{
    public string RoomNumber { get; set; } = default!;
    public string? Name { get; set; }
}