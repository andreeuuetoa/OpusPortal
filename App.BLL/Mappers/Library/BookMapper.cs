using AutoMapper;
using Base.Mappers;
using BLLBook = BLL.DTO.Library.Book;
using DomainBook = Domain.Library.Book;

namespace App.BLL.Mappers.Library;

public class BookMapper : BaseMapper<DomainBook, BLLBook>
{
    public BookMapper(IMapper mapper) : base(mapper)
    {
    }

    public override DomainBook Map(BLLBook? book)
    {
        return new DomainBook
        {
            Title = book?.Title ?? "",
            Authors = book?.Authors ?? "",
            YearReleased = book?.YearReleased ?? 0
        };
    }

    public override BLLBook Map(DomainBook? entity)
    {
        return new BLLBook
        {
            Title = entity?.Title ?? "",
            Authors = entity?.Authors ?? "",
            YearReleased = entity?.YearReleased ?? 0
        };
    }
}