using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Domain.Institutions;

namespace Domain.Concerts;

public class Concert : DomainEntityId
{
    public Guid InstitutionId { get; set; }
    public Institution? Institution { get; set; }

    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime Until { get; set; }

    public ICollection<ConcertOrganizer>? ConcertOrganizers { get; set; }
    public ICollection<PersonAtConcert>? PersonAtConcerts { get; set; }
}