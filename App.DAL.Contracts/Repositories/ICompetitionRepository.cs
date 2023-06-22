using Base.Contracts;
using Base.DTO.Contracts;
using Domain.Competitions;

namespace App.DAL.Contracts.Repositories;

public interface ICompetitionRepository : IBaseRepository<Competition>, ICompetitionRepositoryCustom<Competition>
{
    
}

public interface ICompetitionRepositoryCustom<TEntity>
{
    
}