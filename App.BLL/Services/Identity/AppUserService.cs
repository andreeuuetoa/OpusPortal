using App.BLL.Contracts.Services.Identity;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using Base;
using Base.BLL;
using DomainAppUser = Domain.Identity.AppUser;
using BLLAppUser = BLL.DTO.Identity.AppUser;

namespace App.BLL.Services.Identity;

public class AppUserService : BaseEntityService<DomainAppUser, BLLAppUser, IAppUserRepository>, IAppUserService
{
    private readonly IAppUOW Uow;
    
    public AppUserService(IAppUOW uow, IMapper<DomainAppUser, BLLAppUser> mapper) : base(uow.AppUserRepository, mapper)
    {
        Uow = uow;
    }
}