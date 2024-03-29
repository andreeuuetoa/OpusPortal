﻿using App.BLL.Contracts.Services.Competitions;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories.Competitions;
using Base;
using Base.BLL;
using DomainJuryMember = Domain.Competitions.JuryMember;
using BLLJuryMember = BLL.DTO.Competitions.JuryMember;

namespace App.BLL.Services.Competitions;

public class JuryService : BaseEntityService<DomainJuryMember, BLLJuryMember, IJuryMemberRepository>, IJuryService
{
    private readonly IAppDAL _dal;
    
    public JuryService(IAppDAL dal, IMapper<DomainJuryMember, BLLJuryMember> mapper) : base(dal.JuryMemberRepository, mapper)
    {
        _dal = dal;
    }
}