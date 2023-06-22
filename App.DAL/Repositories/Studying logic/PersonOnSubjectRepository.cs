using App.DAL.Contracts.Repositories;
using Base;
using Domain.Studying_logic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Studying_logic;

public class PersonOnSubjectRepository : BaseRepository<PersonOnSubject, AppDbContext>,
    IPersonOnSubjectRepository
{
    public PersonOnSubjectRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<PersonOnSubject>> All()
    {
        return await RepositoryDbSet
            .Include(s => s.Subject)
            .ThenInclude(s => s!.SubjectTeachers)
            .Include(s => s.AppUser)
            .ToListAsync();
    }

    public async Task<IEnumerable<PersonOnSubject>> AllWithUserId(Guid id)
    {
        return await RepositoryDbSet
            .Include(s => s.Subject)
            .ThenInclude(s => s!.SubjectTeachers)
            .Include(s => s.AppUser)
            .Where(s => s.AppUserId.Equals(id))
            .ToListAsync();
    }

    public override async Task<PersonOnSubject?> Find(Guid id)
    {
        return await RepositoryDbSet
            .Include(s => s.Subject)
            .ThenInclude(s => s!.SubjectTeachers)
            .Include(s => s.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}