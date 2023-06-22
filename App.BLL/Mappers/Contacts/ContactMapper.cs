using App.BLL.Mappers.Identity;
using AutoMapper;
using Base.Mappers;
using BLL.DTO.Identity;
using DomainContact = Domain.Contacts.Contact;
using BLLContact = BLL.DTO.Contacts.Contact;

namespace App.BLL.Mappers.Contacts;

public class ContactMapper : BaseMapper<DomainContact, BLLContact>
{
    private ContactTypeMapper _contactTypeMapper;
    private PersonMapper _personMapper;
    
    public ContactMapper(IMapper mapper) : base(mapper)
    {
        _contactTypeMapper = new ContactTypeMapper(
            new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig())).CreateMapper()
        );
        _personMapper = new PersonMapper(
            new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig())).CreateMapper()
        );
    }

    public override BLLContact? Map(DomainContact? entity)
    {
        if (entity == null) return null;
        return new BLLContact
        {
            PersonFirstName = entity.Person?.FirstName ?? "",
            PersonLastName = entity.Person?.LastName ?? "",
            ContactTypeId = entity.ContactTypeId,
            ContactType = _contactTypeMapper.Map(entity.ContactType),
            Value = entity.Value,
            From = entity.From,
            Until = entity.Until
        };
    }

    public override DomainContact? Map(BLLContact? dalContact)
    {
        if (dalContact == null) return null;
        return new DomainContact
        {
            PersonId = dalContact.PersonId,
            Person = _personMapper.Map(new Person
            {
                Id = dalContact.PersonId,
                FirstName = dalContact.PersonFirstName,
                LastName = dalContact.PersonLastName
            }),
            ContactTypeId = dalContact.ContactTypeId,
            ContactType = _contactTypeMapper.Map(dalContact.ContactType),
            Value = dalContact.Value,
            From = dalContact.From,
            Until = dalContact.Until
        };
    }
}