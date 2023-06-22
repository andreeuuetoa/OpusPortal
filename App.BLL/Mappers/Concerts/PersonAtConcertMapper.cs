using AutoMapper;
using Base.Mappers;
using BLLPersonAtConcert = BLL.DTO.Concerts.PersonAtConcert;
using DomainPersonAtConcert = Domain.Concerts.PersonAtConcert;

namespace App.BLL.Mappers.Concerts;

public class PersonAtConcertMapper : BaseMapper<DomainPersonAtConcert, BLLPersonAtConcert>
{
    public PersonAtConcertMapper(IMapper mapper) : base(mapper)
    {
    }
}