using App.DAL.Contracts.Repositories;
using Base;
using Domain.Competitions;

namespace DAL.Repositories.Competitions;

public class CompetitionRepository : BaseRepository<Competition, AppDbContext>, ICompetitionRepository
{
    public CompetitionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}