using Base.Contracts;
using Base.DTO.Contracts;
using Domain.Library;

namespace App.DAL.Contracts.Repositories;

public interface IBookLentOutRepository : IBaseRepository<BookLentOut>,
    IBookLentOutRepositoryCustom<BookLentOut>
{
    
}

public interface IBookLentOutRepositoryCustom<TEntity> 
{
    Task<IEnumerable<TEntity>> AllWithUserId(Guid id);
}