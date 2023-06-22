using Base.Contracts;
using Domain.Studying_logic;

namespace App.DAL.Contracts.Repositories;

public interface IPersonOnSubjectRepository : IBaseRepository<PersonOnSubject>,
    IPersonOnSubjectRepositoryCustom<PersonOnSubject>
{
    
}

public interface IPersonOnSubjectRepositoryCustom<TEntity>
{
    
}