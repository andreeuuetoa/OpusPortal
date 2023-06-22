using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain.Competitions;

public class Competition : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime Until { get; set; }

    public ICollection<CompetitionOrganizer>? CompetitionOrganizers { get; set; }
    public ICollection<Round>? Rounds { get; set; }
    public ICollection<Jury>? Juries { get; set; }
    public ICollection<Category>? Categories { get; set; }
    public ICollection<PersonAtCompetition>? PersonAtCompetitions { get; set; }
}