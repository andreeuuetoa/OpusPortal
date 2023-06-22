using Base.Domain;

namespace Domain.Studying_logic;

public class ClassInRoom : DomainEntityId
{
    public Guid ClassId { get; set; }
    public Class? Class { get; set; }

    public Guid RoomId { get; set; }
    public Room? Room { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}