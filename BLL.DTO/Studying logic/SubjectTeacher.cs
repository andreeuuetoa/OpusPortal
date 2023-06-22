using BLL.DTO.Identity;

namespace BLL.DTO.Studying_logic;

public class SubjectTeacher
{
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
}