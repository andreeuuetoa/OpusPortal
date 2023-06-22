using Base.DTO;
using BLL.DTO.Contacts;

namespace BLL.DTO.Identity;

public class Person : DTOEntityId
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public ICollection<Contact>? Contacts { get; set; }
}