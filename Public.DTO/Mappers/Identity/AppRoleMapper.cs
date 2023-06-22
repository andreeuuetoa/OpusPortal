using AutoMapper;
using Base.Mappers;
using BLLAppRole = BLL.DTO.Identity.AppRole;
using PublicAppRole = Public.DTO.v1._0.Identity.AppRole;

namespace Public.DTO.Mappers.Identity;

public class AppRoleMapper : BaseMapper<BLLAppRole, PublicAppRole>
{
    public AppRoleMapper(IMapper mapper) : base(mapper)
    {
    }
}