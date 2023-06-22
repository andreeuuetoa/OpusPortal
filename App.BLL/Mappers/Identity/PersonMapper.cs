using App.BLL.Mappers.Contacts;
using AutoMapper;
using Base.Mappers;
using BLLPerson = BLL.DTO.Identity.Person;
using DomainPerson = Domain.Identity.Person;

namespace App.BLL.Mappers.Identity;

public class PersonMapper : BaseMapper<DomainPerson, BLLPerson>
{
    private ContactMapper _contactMapper;
    
    public PersonMapper(IMapper mapper) : base(mapper)
    {
        _contactMapper = new ContactMapper(
            new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig())).CreateMapper()
        );
    }

    public override DomainPerson? Map(BLLPerson? dalPerson)
    {
        if (dalPerson == null) return null;
        return new DomainPerson
        {
            Id = dalPerson.Id,
            FirstName = dalPerson.FirstName,
            LastName = dalPerson.LastName
        };
    }
}