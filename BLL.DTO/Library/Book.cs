using Base.DTO;

namespace BLL.DTO.Library;

public class Book : DTOEntityId
{
    public string Title { get; set; } = default!;
    public string Authors { get; set; } = default!;
    public int YearReleased { get; set; }
}