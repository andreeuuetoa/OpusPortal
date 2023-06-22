using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain.Contacts;

public class ContactType : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    public ICollection<Contact>? Contacts { get; set; }
}