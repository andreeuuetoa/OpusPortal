using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Domain.Identity;

namespace Domain.Contacts;

public class Contact : DomainEntityId
{
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }

    public Guid ContactTypeId { get; set; }
    public ContactType? ContactType { get; set; }

    [MinLength(1)]
    [MaxLength(256)]
    public string Value { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
}