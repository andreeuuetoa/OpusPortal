using Base.Domain;
using Domain.Identity;
using Domain.Institutions;

namespace Domain.Competitions;

public class CompetitionOrganizer : DomainEntityId
{
    public Guid CompetitionId { get; set; }
    public Competition? Competition { get; set; }

    public Guid? InstitutionId { get; set; }
    public Institution? Institution { get; set; }

    public Guid? PersonId { get; set; }
    public Person? Person { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}