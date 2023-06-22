using Base.Contracts;
using Domain.Studying_logic;

namespace App.DAL.Contracts.Repositories;

public interface ICurriculumRepository : IBaseRepository<Curriculum>,
    ICurriculumRepositoryCustom<Curriculum>
{
    
}

public interface ICurriculumRepositoryCustom<TEntity>
{
    
}