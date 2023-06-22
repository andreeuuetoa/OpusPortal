using Base.DTO;
using BLL.DTO.Identity;

namespace BLL.DTO.Studying_logic;

public class PersonOnSubject : DTOEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid SubjectId { get; set; }
    public Subject? Subject { get; set; }

    public double? AverageGrade { get; private set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }

    public void SetAverageGrade(double grade)
    {
        AverageGrade = grade;
    }
}