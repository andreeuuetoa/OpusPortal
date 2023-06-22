using AutoMapper;
using Base.Mappers;
using BLLConcert = BLL.DTO.Concerts.Concert;
using DomainConcert = Domain.Concerts.Concert;

namespace App.BLL.Mappers.Concerts;

public class ConcertMapper : BaseMapper<DomainConcert, BLLConcert>
{
    public ConcertMapper(IMapper mapper) : base(mapper)
    {
    }
}