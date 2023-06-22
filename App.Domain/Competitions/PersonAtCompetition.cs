using Base.Domain;
using Domain.Identity;

namespace Domain.Competitions;

public class PersonAtCompetition : DomainEntityId
{
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }

    public Guid CompetitionId { get; set; }
    public Competition? Competition { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}