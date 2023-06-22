using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain.Studying_logic;

public class Curriculum : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(20)]
    public string CurriculumCode { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }

    public ICollection<PersonOnCurriculum>? PersonOnCurricula { get; set; }
    public ICollection<SubjectInCurriculum>? SubjectInCurricula { get; set; }
}