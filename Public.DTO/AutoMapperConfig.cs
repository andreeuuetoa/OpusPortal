using AutoMapper;

namespace Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<BLL.DTO.Library.Book, v1._0.Library.Book>().ReverseMap();
        CreateMap<BLL.DTO.Library.BookLentOut, v1._0.Library.BookLentOut>().ReverseMap();
        CreateMap<BLL.DTO.Identity.AppUser, v1._0.Identity.AppUser>().ReverseMap();
        CreateMap<BLL.DTO.Identity.AppRole, v1._0.Identity.AppRole>().ReverseMap();
        CreateMap<BLL.DTO.Identity.Person, v1._0.Identity.Person>().ReverseMap();
    }
}