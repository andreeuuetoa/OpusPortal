using App.DAL.Contracts.Repositories;
using Base;
using Domain.Library;

namespace DAL.Repositories.Library;

public class BookRepository : BaseRepository<Book, AppDbContext>, IBookRepository
{
    public BookRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}