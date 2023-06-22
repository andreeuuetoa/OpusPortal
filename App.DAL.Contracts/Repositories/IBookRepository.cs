using Base.Contracts;
using Domain.Library;

namespace App.DAL.Contracts.Repositories;

/// <summary>
/// Custom methods for book repository.
/// </summary>
public interface IBookRepository : IBaseRepository<Book>
{
    
}