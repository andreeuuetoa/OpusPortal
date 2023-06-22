using App.DAL.Contracts.Repositories;
using Base;
using Domain.Library;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Library;

public class BookLentOutRepository: BaseRepository<BookLentOut, AppDbContext>,
    IBookLentOutRepository
{
    public BookLentOutRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }

    public override async Task<IEnumerable<BookLentOut>> All()
    {
        return await RepositoryDbSet
            .Include(bookLentOut => bookLentOut.Book)
            .Include(bookLentOut => bookLentOut.AppUser)
            .OrderBy(bookLentOut => bookLentOut.Book!.Title)
            .ThenBy(bookLentOut => bookLentOut.Book!.Authors)
            .ToListAsync();
    }

    public async Task<IEnumerable<BookLentOut>> AllWithUserId(Guid id)
    {
        return await RepositoryDbSet
            .Include(bookLentOut => bookLentOut.Book)
            .Include(bookLentOut => bookLentOut.AppUser)
            .Where(b => b.AppUserId == id)
            .OrderBy(bookLentOut => bookLentOut.Book!.Title)
            .ThenBy(bookLentOut => bookLentOut.Book!.Authors)
            .ToListAsync();
    }

    public override async Task<BookLentOut?> Find(Guid id)
    {
        return await RepositoryDbSet
            .Include(i => i.Book)
            .Include(i => i.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}