using Base.Contracts;
using Domain.Concerts;

namespace App.DAL.Contracts.Repositories;

public interface IPerformanceRepository : IBaseRepository<PersonAtConcert>,
    IPerformanceRepositoryCustom<PersonAtConcert>
{
    
}

public interface IPerformanceRepositoryCustom<TEntity>
{
    
}