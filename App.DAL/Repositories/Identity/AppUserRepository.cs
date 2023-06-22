using App.DAL.Contracts.Repositories;
using Base;
using Domain.Identity;

namespace DAL.Repositories.Identity;

public class AppUserRepository : BaseRepository<AppUser, AppDbContext>, IAppUserRepository
{
    public AppUserRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}