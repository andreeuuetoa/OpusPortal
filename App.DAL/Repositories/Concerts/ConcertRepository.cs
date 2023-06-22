using App.DAL.Contracts.Repositories;
using Base;
using Domain.Concerts;

namespace DAL.Repositories.Concerts;

public class ConcertRepository : BaseRepository<Concert, AppDbContext>, IConcertRepository
{
    public ConcertRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}