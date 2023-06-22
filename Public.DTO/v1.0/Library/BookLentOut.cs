using Base.DTO;
using Public.DTO.v1._0.Identity;

namespace Public.DTO.v1._0.Library;

public class BookLentOut : DTOEntityId
{
    public Guid BookId { get; set; }
    public Book? Book { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public DateTime LentAt { get; set; }
    public DateTime ReturnAt { get; set; }
}