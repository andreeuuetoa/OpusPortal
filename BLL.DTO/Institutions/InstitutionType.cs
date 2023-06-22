using System.ComponentModel.DataAnnotations;
using Base.DTO;

namespace BLL.DTO.Institutions;

public class InstitutionType : DTOEntityId
{
    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public ICollection<Institution>? Institutions { get; set; }
}