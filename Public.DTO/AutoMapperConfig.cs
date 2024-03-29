﻿using AutoMapper;

namespace Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<BLL.DTO.Library.Book, v1._0.Library.Book>().ReverseMap();
        CreateMap<BLL.DTO.Library.BookLentOut, v1._0.Library.BookLentOut>().ReverseMap();
        CreateMap<BLL.DTO.Identity.AppUser, v1._0.Identity.AppUser>().ReverseMap();
        CreateMap<BLL.DTO.Identity.AppRole, v1._0.Identity.AppRole>().ReverseMap();
        CreateMap<BLL.DTO.Competitions.Competition, v1._0.Competitions.Competition>().ReverseMap();
        CreateMap<BLL.DTO.Concerts.Concert, v1._0.Concerts.Concert>().ReverseMap();
        CreateMap<BLL.DTO.Rooms.Room, v1._0.Rooms.Room>().ReverseMap();
    }
}