using App.DAL.Contracts.Repositories;
using Base;
using Domain.Identity;

namespace DAL.Repositories.Identity;

public class AppRoleRepository : BaseRepository<AppRole, AppDbContext>, IAppRoleRepository
{
    public AppRoleRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}