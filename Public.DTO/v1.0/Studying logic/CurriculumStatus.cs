using Base.DTO;

namespace Public.DTO.v1._0.Studying_logic;

public class CurriculumStatus : DTOEntityId
{
    public string CurriculumName { get; set; } = default!;
    public double WeightedAverageGrade { get; set; }
    public int ECTSAchieved { get; set; }
    public int Semester { get; set; }
}