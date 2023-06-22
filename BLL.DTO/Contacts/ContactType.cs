using Base.DTO;

namespace BLL.DTO.Contacts;

public class ContactType : DTOEntityId
{
    public string Name { get; set; } = default!;
}