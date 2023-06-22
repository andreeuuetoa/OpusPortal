using App.DAL.Contracts.Repositories;
using Base;
using Domain.Contacts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Contacts;

public class ContactRepository : BaseRepository<Contact, AppDbContext>,
    IContactRepository
{
    public ContactRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Contact>> All()
    {
        return await RepositoryDbSet
            .Include(x => x.Person)
            .OrderBy(x => x.PersonId)
            .ToListAsync();
    }
}