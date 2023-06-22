using AutoMapper;
using Base.Mappers;
using BLLPersonOnCurriculum = BLL.DTO.Studying_logic.PersonOnCurriculum;
using DomainPersonOnCurriculum = Domain.Studying_logic.PersonOnCurriculum;

namespace App.BLL.Mappers.Studying_logic;

public class PersonOnCurriculumMapper : BaseMapper<DomainPersonOnCurriculum, BLLPersonOnCurriculum>
{
    public PersonOnCurriculumMapper(IMapper mapper) : base(mapper)
    {
    }
}