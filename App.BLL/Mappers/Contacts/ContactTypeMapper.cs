using AutoMapper;
using Base.Mappers;
using BLLContactType = BLL.DTO.Contacts.ContactType;
using DomainContactType = Domain.Contacts.ContactType;

namespace App.BLL.Mappers.Contacts;

public class ContactTypeMapper : BaseMapper<DomainContactType, BLLContactType>
{
    public ContactTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}