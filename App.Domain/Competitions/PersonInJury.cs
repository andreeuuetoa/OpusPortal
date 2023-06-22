using Base.Domain;
using Domain.Identity;

namespace Domain.Competitions;

public class PersonInJury : DomainEntityId
{
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }

    public Guid JuryId { get; set; }
    public Jury? Jury { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }
}