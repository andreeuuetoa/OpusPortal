﻿using App.BLL.Contracts.Services.Library;
using App.DAL.Contracts;
using App.DAL.Contracts.Repositories;
using App.DAL.Contracts.Repositories.Library;
using Base;
using Base.BLL;
using BLLBook = BLL.DTO.Library.Book;
using DomainBook = Domain.Library.Book;

namespace App.BLL.Services.Library;

public class BookService : BaseEntityService<DomainBook, BLLBook, IBookRepository>, IBookService
{
    protected readonly IAppDAL Dal;

    public BookService(IAppDAL dal, IMapper<DomainBook, BLLBook> mapper) : base(dal.BookRepository, mapper)
    {
        Dal = dal;
    }
}