using Base.DTO;
using BLL.DTO.Identity;

namespace BLL.DTO.Library;

public class BookLentOut : DTOEntityId
{
    public Guid BookId { get; set; }
    public Book? Book { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public DateTime LentAt { get; set; }
    public DateTime ReturnAt { get; set; }
}