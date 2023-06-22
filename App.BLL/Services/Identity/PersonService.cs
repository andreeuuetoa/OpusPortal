using App.BLL.Contracts.Services.Identity;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using Base;
using Base.BLL;
using DomainPerson = Domain.Identity.Person;
using BLLPerson = BLL.DTO.Identity.Person;

namespace App.BLL.Services.Identity;

public class PersonService : BaseEntityService<DomainPerson, BLLPerson, IPersonRepository>, IPersonService
{
    protected readonly IAppUOW Uow;
    
    public PersonService(IAppUOW uow, IMapper<DomainPerson, BLLPerson> mapper) : base(uow.PersonRepository, mapper)
    {
        Uow = uow;
    }
}