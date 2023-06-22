using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Domain.Competitions;
using Domain.Concerts;

namespace Domain.Institutions;

public class Institution : DomainEntityId
{
    public Guid InstitutionTypeId { get; set; }
    public InstitutionType? InstitutionType { get; set; }

    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(30)]
    public string RegistryCode { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(256)]
    public string Address { get; set; } = default!;
    
    public DateTime EstablishedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    
    public ICollection<Concert>? Concerts { get; set; }
    public ICollection<ConcertOrganizer>? ConcertOrganizers { get; set; }
    public ICollection<Round>? Rounds { get; set; }
    public ICollection<CompetitionOrganizer>? CompetitionOrganizers { get; set; }
    public ICollection<InstitutionAcronym>? Acronyms { get; set; }
}