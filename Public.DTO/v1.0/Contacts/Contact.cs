using Base.DTO;

namespace Public.DTO.v1._0.Contacts;

public class Contact : DTOEntityId
{
    public string PersonName { get; set; } = default!;
    public string Value { get; set; } = default!;
    public string Type { get; set; } = default!;
}