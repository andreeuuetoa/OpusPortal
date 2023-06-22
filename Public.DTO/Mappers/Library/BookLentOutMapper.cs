using AutoMapper;
using Base.Mappers;
using BLL.DTO.Library;
using Public.DTO.Mappers.Identity;
using BLLBookLentOut = BLL.DTO.Library.BookLentOut;
using PublicBookLentOut = Public.DTO.v1._0.Library.BookLentOut;

namespace Public.DTO.Mappers.Library;

public class BookLentOutMapper : BaseMapper<BLLBookLentOut, PublicBookLentOut>
{
    public BookLentOutMapper(IMapper mapper) : base(mapper)
    {
    }
}