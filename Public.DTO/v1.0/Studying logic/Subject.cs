using Base.DTO;

namespace Public.DTO.v1._0.Studying_logic;

public class Subject : DTOEntityId
{
    public string Name { get; set; } = default!;
    public string Teachers { get; set; } = default!;
    public int ECTS { get; set; }
}