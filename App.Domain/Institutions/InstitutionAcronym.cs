using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Domain.Institutions;

namespace Domain;

public class InstitutionAcronym : DomainEntityId
{
    public Guid InstitutionId { get; set; }
    public Institution? Institution { get; set; }
    
    [MinLength(1)]
    [MaxLength(64)]
    public string Acronym { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
}