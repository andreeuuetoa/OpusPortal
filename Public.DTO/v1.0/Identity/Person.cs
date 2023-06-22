using Base.DTO;

namespace Public.DTO.v1._0.Identity;

public class Person : DTOEntityId
{
    public Guid PersonId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}