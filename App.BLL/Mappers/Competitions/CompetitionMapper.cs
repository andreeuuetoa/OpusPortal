using AutoMapper;
using Base.Mappers;
using DomainCompetition = Domain.Competitions.Competition;
using BLLCompetition = BLL.DTO.Competitions.Competition;

namespace App.BLL.Mappers.Competitions;

public class CompetitionMapper : BaseMapper<DomainCompetition, BLLCompetition>
{
    public CompetitionMapper(IMapper mapper) : base(mapper)
    {
    }
}