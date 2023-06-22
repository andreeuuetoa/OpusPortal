using Base.Contracts;
using Base.DTO.Contracts;
using Domain.Contacts;

namespace App.DAL.Contracts.Repositories;

public interface IContactRepository : IBaseRepository<Contact>,
    IContactRepositoryCustom<Contact>
{
    
}

public interface IContactRepositoryCustom<TEntity>
{
    
}
