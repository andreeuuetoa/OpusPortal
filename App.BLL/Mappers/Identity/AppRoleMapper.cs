using AutoMapper;
using Base.Mappers;
using BLLAppRole = BLL.DTO.Identity.AppRole;
using DomainAppRole = Domain.Identity.AppRole;

namespace App.BLL.Mappers.Identity;

public class AppRoleMapper : BaseMapper<DomainAppRole, BLLAppRole>
{
    public AppRoleMapper(IMapper mapper) : base(mapper)
    {
    }
}