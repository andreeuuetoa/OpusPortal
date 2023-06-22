using Base.Contracts;
using Base.DTO.Contracts;
using Domain.Concerts;

namespace App.DAL.Contracts.Repositories;

public interface IConcertRepository : IBaseRepository<Concert>, IConcertRepositoryCustom<Concert>
{
    
}

public interface IConcertRepositoryCustom<TEntity>
{
    
}