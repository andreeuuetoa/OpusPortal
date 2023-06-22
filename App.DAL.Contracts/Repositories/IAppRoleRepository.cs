using Base.Contracts;
using Domain.Identity;

namespace App.DAL.Contracts.Repositories;

public interface IAppRoleRepository : IBaseRepository<AppRole>,
    IAppRoleRepositoryCustom<AppRole>
{
    
}

public interface IAppRoleRepositoryCustom<TEntity>
{
    
}