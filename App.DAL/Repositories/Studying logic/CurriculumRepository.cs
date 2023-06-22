using App.DAL.Contracts.Repositories;
using Base;
using Domain.Studying_logic;

namespace DAL.Repositories.Studying_logic;

public class CurriculumRepository : BaseRepository<Curriculum, AppDbContext>,
    ICurriculumRepository
{
    public CurriculumRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}