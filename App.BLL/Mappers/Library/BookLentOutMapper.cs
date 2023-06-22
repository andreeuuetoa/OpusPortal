using AutoMapper;
using Base.Mappers;
using BLLBook = BLL.DTO.Library.Book;
using DomainBook = Domain.Library.Book;
using BLLBookLentOut = BLL.DTO.Library.BookLentOut;
using DomainBookLentOut = Domain.Library.BookLentOut;

namespace App.BLL.Mappers.Library;

public class BookLentOutMapper : BaseMapper<DomainBookLentOut, BLLBookLentOut>
{
    public BookLentOutMapper(IMapper mapper) : base(mapper)
    {
    }
}