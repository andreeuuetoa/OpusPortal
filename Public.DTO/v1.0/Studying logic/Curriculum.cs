using Base.DTO;

namespace Public.DTO.v1._0.Studying_logic;

public class Curriculum : DTOEntityId
{
    public string Name { get; set; } = default!;
    public string CurriculumCode { get; set; } = default!;
}