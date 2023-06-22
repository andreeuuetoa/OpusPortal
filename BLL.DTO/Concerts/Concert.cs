using System.ComponentModel.DataAnnotations;
using Base.DTO;
using BLL.DTO.Institutions;

namespace BLL.DTO.Concerts;

public class Concert : DTOEntityId
{
    public Guid InstitutionId { get; set; }
    public Institution? Institution { get; set; }

    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}