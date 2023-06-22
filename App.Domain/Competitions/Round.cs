using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Domain.Institutions;

namespace Domain.Competitions;

public class Round : DomainEntityId
{
    public Guid CompetitionId { get; set; }
    public Competition? Competition { get; set; }

    public Guid InstitutionId { get; set; }
    public Institution? Institution { get; set; }

    public Guid JuryId { get; set; }
    public Jury? Jury { get; set; }

    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}