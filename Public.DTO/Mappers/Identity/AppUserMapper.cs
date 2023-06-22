using AutoMapper;
using Base.Mappers;
using Public.DTO.v1._0.Identity;
using BLLAppUser = BLL.DTO.Identity.AppUser;
using PublicAppUser = Public.DTO.v1._0.Identity.AppUser;

namespace Public.DTO.Mappers.Identity;

public class AppUserMapper : BaseMapper<BLLAppUser, PublicAppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}