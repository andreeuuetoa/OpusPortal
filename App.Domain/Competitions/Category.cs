using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain.Competitions;

public class Category : DomainEntityId
{
    public Guid CompetitionId { get; set; }
    public Competition? Competition { get; set; }

    public Guid JuryId { get; set; }
    public Jury? Jury { get; set; }

    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public ICollection<PersonAtCompetition>? PersonAtCompetitions { get; set; }
}