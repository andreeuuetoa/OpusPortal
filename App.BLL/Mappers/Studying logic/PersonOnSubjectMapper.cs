using AutoMapper;
using Base.Mappers;
using DomainPersonOnSubject = Domain.Studying_logic.PersonOnSubject;
using BLLPersonOnSubject = BLL.DTO.Studying_logic.PersonOnSubject;

namespace App.BLL.Mappers.Studying_logic;

public class PersonOnSubjectMapper : BaseMapper<DomainPersonOnSubject, BLLPersonOnSubject>
{
    public PersonOnSubjectMapper(IMapper mapper) : base(mapper)
    {
    }
}