using System.ComponentModel.DataAnnotations;
using Base.Domain;
using Domain.Competitions;
using Domain.Concerts;
using Domain.Contacts;

namespace Domain.Identity;

public class Person : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(256)]
    public string FirstName { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(256)]
    public string LastName { get; set; } = default!;
    
    public ICollection<AppUser>? AppUsers { get; set; }
    public ICollection<PersonInJury>? PersonInJuries { get; set; }
    public ICollection<CompetitionOrganizer>? CompetitionOrganizers { get; set; }
    public ICollection<ConcertOrganizer>? ConcertOrganizers { get; set; }
    public ICollection<Contact>? Contacts { get; set; }
}