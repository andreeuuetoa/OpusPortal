using Base.DTO;

namespace BLL.DTO.Studying_logic;

public class Curriculum : DTOEntityId
{
    public string Name { get; set; } = default!;

    public string CurriculumCode { get; set; } = default!;

    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
}