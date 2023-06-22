using AutoMapper;
using BLL.DTO.Competitions;
using BLL.DTO.Concerts;
using BLL.DTO.Contacts;
using BLL.DTO.Identity;
using BLL.DTO.Library;
using BLL.DTO.Studying_logic;

namespace App.BLL.Mappers;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Domain.Identity.AppRole, AppRole>().ReverseMap();
        CreateMap<Domain.Identity.AppUser, AppUser>().ReverseMap();
        CreateMap<Domain.Library.Book, Book>().ReverseMap();
        CreateMap<Domain.Competitions.Competition, Competition>().ReverseMap();
        CreateMap<Domain.Concerts.Concert, Concert>().ReverseMap();
        CreateMap<Domain.Contacts.Contact, Contact>().ReverseMap();
        CreateMap<Domain.Studying_logic.PersonOnCurriculum, PersonOnCurriculum>().ReverseMap();
        CreateMap<Domain.Library.BookLentOut, BookLentOut>().ReverseMap();
        CreateMap<Domain.Concerts.PersonAtConcert, PersonAtConcert>().ReverseMap();
        CreateMap<Domain.Identity.Person, Person>().ReverseMap();
        CreateMap<Domain.Studying_logic.PersonOnSubject, PersonOnSubject>().ReverseMap();
    }
}