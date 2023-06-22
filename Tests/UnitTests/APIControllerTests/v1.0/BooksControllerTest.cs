using App.BLL;
using App.BLL.Contracts;
using AutoMapper;
using Base;
using DAL;
using DAL.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Public.DTO;
using Public.DTO.Mappers.Library;
using Public.DTO.v1._0.Library;
using WebApp.APIControllers.v1._0;
using BLLBook = BLL.DTO.Library.Book;
using PublicBook = Public.DTO.v1._0.Library.Book;

namespace Tests.UnitTests.APIControllerTests.v1._0;

public class BooksControllerTest
{
    private readonly IAppBLL _bll;
    private readonly IMapper _mapper;
    
    public BooksControllerTest()
    {
        // set up mock database - in-memory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        var ctx = new AppDbContext(optionsBuilder.Options);

        // reset db
        ctx.Database.EnsureDeleted();
        ctx.Database.EnsureCreated();

        var uow = new AppUOW(ctx);

        _mapper = new MapperConfiguration(mc => mc.AddProfile(new AutoMapperConfig())).CreateMapper();

        _bll = new AppBLL(uow, _mapper);
    }
    
    [Fact(DisplayName = "GET - api/v1.0/Books")]
    public async void TestGetAllBooksEmpty()
    {
        var controller = new BooksController(_bll, _mapper);

        var booksResult = await controller.GetBooks();
        
        Assert.NotNull(booksResult);
        
        var booksObjectResult = booksResult.Result as OkObjectResult;
        
        Assert.NotNull(booksObjectResult);

        var books = booksObjectResult.Value as IEnumerable<Book>;
        
        Assert.NotNull(books);
        Assert.Empty(books);
    }

    [Fact(DisplayName = "GET - api/v1.0/Books with one book")]
    public async void TestGetAllBooksWithOneBook()
    {
        var controller = new BooksController(_bll, _mapper);
        
        var book = new Book
        {
            Title = "Ujedus ja väärikus",
            Authors = "Dag Solstad",
            YearReleased = 2010
        };

        var postBookResult = await controller.PostBook(book);
        
        Assert.NotNull(postBookResult);

        var booksResult = await controller.GetBooks();
        
        Assert.NotNull(booksResult);
        
        var booksObjectResult = booksResult.Result as OkObjectResult;
        
        Assert.NotNull(booksObjectResult);

        var books = booksObjectResult.Value as IEnumerable<Book>;
        
        Assert.NotNull(books);
        var bookList = books.ToList();
        Assert.NotEmpty(bookList);
        Assert.Single(bookList);
    }
}