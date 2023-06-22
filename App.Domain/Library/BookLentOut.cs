using Base.Domain;
using Domain.Identity;

namespace Domain.Library;

public class BookLentOut : DomainEntityId
{
    public Guid BookId { get; set; }
    public Book? Book { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public DateTime LentAt { get; set; }
    public DateTime ReturnAt { get; set; }
    public DateTime? ReturnedAt { get; set; }
}