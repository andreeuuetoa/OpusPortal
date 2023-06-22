using App.DAL.Contracts.Repositories;
using Base;
using Domain.Institutions;

namespace DAL.Repositories.Institutions;

public class InstitutionRepository : BaseRepository<Institution, AppDbContext>, IInstitutionRepository
{
    public InstitutionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}