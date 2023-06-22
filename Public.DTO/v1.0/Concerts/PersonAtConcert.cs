using Base.DTO;

namespace Public.DTO.v1._0.Concerts;

public class PersonAtConcert : DTOEntityId
{
    public string Name { get; set; } = default!;
    public string Location { get; set; } = default!;
}