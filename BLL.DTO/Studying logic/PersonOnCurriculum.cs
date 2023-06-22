using Base.DTO;
using BLL.DTO.Identity;

namespace BLL.DTO.Studying_logic;

public class PersonOnCurriculum : DTOEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid CurriculumId { get; set; }
    public Curriculum? Curriculum { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}