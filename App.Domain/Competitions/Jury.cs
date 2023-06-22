using Base.Domain;

namespace Domain.Competitions;

public class Jury : DomainEntityId
{
    public Guid CompetitionId { get; set; }
    public Competition? Competition { get; set; }

    public DateTime From { get; set; }
    public DateTime Until { get; set; }

    public ICollection<Round>? Rounds { get; set; }
    public ICollection<PersonInJury>? PersonInJuries { get; set; }
    public ICollection<Category>? Categories { get; set; }
}