using Base.Domain;
using Domain.Identity;

namespace Domain.Concerts;

public class PersonAtConcert : DomainEntityId
{
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }

    public Guid ConcertId { get; set; }
    public Concert? Concert { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}