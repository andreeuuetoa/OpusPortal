using Base.Domain;

namespace Domain.Studying_logic;

public class Class : DomainEntityId
{
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public bool IsNotInSchedule { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }

    public ICollection<TeacherInClass>? TeacherInClasses { get; set; }
}