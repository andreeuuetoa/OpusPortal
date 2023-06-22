using Base.DTO;

namespace BLL.DTO.Institutions;

public class Institution : DTOEntityId
{
    public Guid InstitutionTypeId { get; set; }
    public InstitutionType? InstitutionType { get; set; }
    
    public string Name { get; set; } = default!;
    
    public string RegistryCode { get; set; } = default!;
    
    public string Address { get; set; } = default!;
    
    public DateTime EstablishedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}