using Base.Contracts;
using Base.DTO.Contracts;
using Domain.Identity;

namespace App.DAL.Contracts.Repositories;

public interface IPersonRepository : IBaseRepository<Person>,
    IPersonRepositoryCustom<Person>
{
    
}

public interface IPersonRepositoryCustom<TEntity>
{
    
}