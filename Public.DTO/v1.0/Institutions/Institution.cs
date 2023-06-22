using Base.DTO;

namespace Public.DTO.v1._0.Institutions;

public class Institution : DTOEntityId
{
    public string Name { get; set; } = default!;
    public string RegistryCode { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? Acronym { get; set; }
    public string Type { get; set; } = default!;
}