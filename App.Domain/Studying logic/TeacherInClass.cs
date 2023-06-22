using Base.Domain;

namespace Domain.Studying_logic;

public class TeacherInClass : DomainEntityId
{
    public Guid SubjectTeacherId { get; set; }
    public SubjectTeacher? SubjectTeacher { get; set; }

    public Guid ClassId { get; set; }
    public Class? Class { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}