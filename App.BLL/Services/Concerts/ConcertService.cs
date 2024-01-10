﻿using App.BLL.Contracts.Services.Concerts;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories.Concerts;
using Base;
using Base.BLL;
using DomainConcert = Domain.Concerts.Concert;
using BLLConcert = BLL.DTO.Concerts.Concert;

namespace App.BLL.Services.Concerts;

public class ConcertService : BaseEntityService<DomainConcert, BLLConcert, IConcertRepository>, IConcertService
{
    private readonly IAppUOW _uow;
    
    public ConcertService(IAppUOW uow, IMapper<DomainConcert, BLLConcert> mapper) : base(uow.ConcertRepository, mapper)
    {
        _uow = uow;
    }
}