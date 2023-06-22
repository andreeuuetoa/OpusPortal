using Base.Contracts;
using Base.DTO.Contracts;
using Domain.Studying_logic;

namespace App.DAL.Contracts.Repositories;

public interface IPersonOnCurriculumRepository : IBaseRepository<PersonOnCurriculum>,
    ICurriculumRepositoryCustom<PersonOnCurriculum>
{
    
}

public interface IPersonOnCurriculumRepositoryCustom<TEntity>
{
    
}