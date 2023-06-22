using App.DAL.Contracts.Repositories;
using Base;
using Domain.Identity;

namespace DAL.Repositories.Identity;

public class PersonRepository : BaseRepository<Person, AppDbContext>,
    IPersonRepository
{
    public PersonRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}