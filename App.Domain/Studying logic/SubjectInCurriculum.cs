using Base.Domain;

namespace Domain.Studying_logic;

public class SubjectInCurriculum : DomainEntityId
{
    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public Guid CurriculumId { get; set; }
    public Curriculum? Curriculum { get; set; }

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
}