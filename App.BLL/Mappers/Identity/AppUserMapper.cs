using AutoMapper;
using Base.Mappers;
using BLLAppUser = BLL.DTO.Identity.AppUser;
using DomainAppUser = Domain.Identity.AppUser;

namespace App.BLL.Mappers.Identity;

public class AppUserMapper : BaseMapper<DomainAppUser, BLLAppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}