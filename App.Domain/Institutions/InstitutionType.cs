using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain.Institutions;

public class InstitutionType : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public ICollection<Institution>? Institutions { get; set; }
}