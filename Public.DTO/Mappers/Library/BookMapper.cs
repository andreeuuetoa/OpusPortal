using AutoMapper;
using Base.Mappers;

namespace Public.DTO.Mappers.Library;

public class BookMapper : BaseMapper<BLL.DTO.Library.Book, v1._0.Library.Book>
{
    public BookMapper(IMapper mapper) : base(mapper)
    {
    }
}