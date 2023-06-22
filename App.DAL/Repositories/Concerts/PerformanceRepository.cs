using App.DAL.Contracts.Repositories;
using Base;
using Domain.Concerts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Concerts;

public class PerformanceRepository : BaseRepository<PersonAtConcert, AppDbContext>,
    IPerformanceRepository
{
    public PerformanceRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<PersonAtConcert>> All()
    {
        return await RepositoryDbSet
            .Include(i => i.Concert)
            .Include(i => i.Person)
            .ToListAsync();
    }
}