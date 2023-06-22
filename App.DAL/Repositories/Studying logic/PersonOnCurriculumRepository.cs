using App.DAL.Contracts.Repositories;
using Base;
using Domain.Studying_logic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Studying_logic;

public class PersonOnCurriculumRepository : BaseRepository<PersonOnCurriculum, AppDbContext>,
    IPersonOnCurriculumRepository
{
    public PersonOnCurriculumRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<PersonOnCurriculum>> All()
    {
        return await RepositoryDbSet
            .Include(i => i.AppUser)
            .Include(i => i.Curriculum)
            .ToListAsync();
    }

    public async Task<PersonOnCurriculum?> FindWithUserId(Guid userId)
    {
        return await RepositoryDbSet
            .Include(i => i.AppUser)
            .Include(i => i.Curriculum)
            .FirstOrDefaultAsync(m => m.AppUserId == userId);
    }
}