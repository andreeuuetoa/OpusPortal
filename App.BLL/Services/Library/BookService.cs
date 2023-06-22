using App.BLL.Contracts.Services;
using App.BLL.Contracts.Services.Library;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using Base;
using Base.BLL;
using BLLBook = BLL.DTO.Library.Book;
using DomainBook = Domain.Library.Book;

namespace App.BLL.Services.Library;

public class BookService : BaseEntityService<DomainBook, BLLBook, IBookRepository>, IBookService
{
    protected readonly IAppUOW Uow;

    public BookService(IAppUOW uow, IMapper<DomainBook, BLLBook> mapper) : base(uow.BookRepository, mapper)
    {
        Uow = uow;
    }
}