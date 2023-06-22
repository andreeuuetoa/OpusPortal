using Base.DTO;

namespace BLL.DTO.Contacts;

public class Contact : DTOEntityId
{
    public Guid PersonId { get; set; }
    public string PersonFirstName { get; set; } = default!;
    public string PersonLastName { get; set; } = default!;

    public Guid ContactTypeId { get; set; }
    public ContactType? ContactType { get; set; }

    public string Value { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
}