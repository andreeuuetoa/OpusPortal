using Base.DTO;

namespace Public.DTO.v1._0.Library;

public class Book : DTOEntityId
{
    public string Title { get; set; } = default!;
    public string Authors { get; set; } = default!;
    public int YearReleased { get; set; }
}