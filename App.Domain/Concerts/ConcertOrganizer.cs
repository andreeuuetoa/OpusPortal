using Base.Domain;
using Domain.Identity;
using Domain.Institutions;

namespace Domain.Concerts;

public class ConcertOrganizer : DomainEntityId
{
    public Guid ConcertId { get; set; }
    public Concert? Concert { get; set; }

    public Guid? InstitutionId { get; set; }
    public Institution? Institution { get; set; }

    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}