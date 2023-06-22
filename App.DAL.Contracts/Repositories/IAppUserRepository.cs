using Base.Contracts;
using Domain.Identity;

namespace App.DAL.Contracts.Repositories;

public interface IAppUserRepository : IBaseRepository<AppUser>,
    IAppUserRepositoryCustom<AppUser>
{
    
}

public interface IAppUserRepositoryCustom<TEntity>
{
    
}