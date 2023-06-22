using Base.DTO;

namespace BLL.DTO.Studying_logic;

public class Subject : DTOEntityId
{
    public string Name { get; set; } = default!;

    public string SubjectCode { get; set; } = default!;

    public int ECTS { get; set; }

    public ICollection<SubjectTeacher>? SubjectTeachers { get; set; }
}