﻿using App.BLL.Contracts.Services.Concerts;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories.Concerts;
using Base;
using Base.BLL;
using DomainAppUserAtConcert = Domain.Concerts.AppUserAtConcert;
using BLLAppUserAtConcert = BLL.DTO.Concerts.AppUserAtConcert;

namespace App.BLL.Services.Concerts;

public class AppUserAtConcertService : BaseEntityService<DomainAppUserAtConcert, BLLAppUserAtConcert, IAppUserAtConcertRepository>, IAppUserAtConcertService
{
    private readonly IAppDAL _dal;
    
    public AppUserAtConcertService(IAppDAL dal, IMapper<DomainAppUserAtConcert, BLLAppUserAtConcert> mapper) : base(dal.AppUserAtConcertRepository, mapper)
    {
        _dal = dal;
    }
    
    public async Task<IEnumerable<BLLAppUserAtConcert>> AllWithUserId(Guid id)
    {
        return (await _dal.AppUserAtConcertRepository.AllWithUserId(id)).Select(e => Mapper.Map(e));
    }
}