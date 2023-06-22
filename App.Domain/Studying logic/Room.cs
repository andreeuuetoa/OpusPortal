using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain.Studying_logic;

public class Room : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(20)]
    public string RoomNumber { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(256)]
    public string? Name { get; set; }

    public ICollection<ClassInRoom>? ClassInRooms { get; set; }
}