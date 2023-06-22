using Base.DTO;
using BLL.DTO.Identity;

namespace BLL.DTO.Concerts;

public class PersonAtConcert : DTOEntityId
{
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }

    public Guid ConcertId { get; set; }
    public Concert? Concert { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}