using Base.Domain;
using Domain.Identity;

namespace Domain.Studying_logic;

public class SubjectTeacher : DomainEntityId
{
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }

    public ICollection<TeacherInClass>? TeacherInClasses { get; set; }
}