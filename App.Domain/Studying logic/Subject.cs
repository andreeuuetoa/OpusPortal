using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace Domain.Studying_logic;

public class Subject : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(256)]
    public string Name { get; set; } = default!;

    [MinLength(1)]
    [MaxLength(256)]
    public string SubjectCode { get; set; } = default!;

    public int ECTS { get; set; }

    public ICollection<SubjectTeacher>? SubjectTeachers { get; set; }
    public ICollection<SubjectInCurriculum>? SubjectInCurricula { get; set; }
}