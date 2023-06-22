using AutoMapper;
using DAL;
using DAL.Repositories.Library;
using Domain.Identity;
using Domain.Library;
using Microsoft.EntityFrameworkCore;
using AutoMapperConfig = App.BLL.Mappers.AutoMapperConfig;

namespace Tests.UnitTests.Repositories;

public class BookLentOutRepositoryTest : IDisposable
{
    private readonly AppDbContext _ctx;
    private readonly List<AppUser> _appUsers;
    private readonly List<Book> _exampleBooks;

    public BookLentOutRepositoryTest()
    {
        // set up mock database - in-memory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var studentRole = new AppRole
        {
            Name = "Student"
        };
        var mariUser = new AppUser
        {
            PersonId = mari.Id,
            Person = mari,
            AppRoleId = studentRole.Id,
            AppRole = studentRole,
            From = DateTime.Parse("01/09/2015"),
            Until = DateTime.Parse("21/06/2021")
        };
        var juhanUser = new AppUser
        {
            PersonId = juhan.Id,
            Person = juhan,
            AppRoleId = studentRole.Id,
            AppRole = studentRole,
            From = DateTime.Parse("01/09/2016"),
            Until = DateTime.Parse("21/06/2023")
        };
        _appUsers = new List<AppUser>
        {
            mariUser, juhanUser
        };
        _exampleBooks = new List<Book>
        {
            new()
            {
                Title = "Twentieth-Century Harmony",
                Authors = "Vincent Persichetti",
                YearReleased = 1961
            },
            new()
            {
                Title = "The Illustrated Lives of the Great Composers: Tchaikovsky",
                Authors = "Simon Mundy",
                YearReleased = 1995
            },
            new()
            {
                Title = "Süiten für Violoncello solo, BWV 1007-1012",
                Authors = "Johann Sebastian Bach",
                YearReleased = 1723
            },
            new()
            {
                Title = "Ujedus ja väärikus",
                Authors = "Dag Solstad",
                YearReleased = 2010
            },
            new()
            {
                Title = "Varjuteater",
                Authors = "Viivi Luik",
                YearReleased = 2010
            }
        };
    }
    
    [Fact]
    public async void TestLibraryRepositoryIsNotNullAndEmpty()
    {
        var testLibraryRepository = new BookLentOutRepository(_ctx);
        var repoEnumerable = await testLibraryRepository.All();
        Assert.NotNull(repoEnumerable);
        Assert.Empty(repoEnumerable);
    }

    [Fact]
    public async void TestAddOneLentOutBookToRepo()
    {
        var testLibraryRepository = new BookLentOutRepository(_ctx);
        var exampleBook = _exampleBooks[2];
        var mariLends = new BookLentOut
        {
            Id = Guid.NewGuid(),
            BookId = exampleBook.Id,
            Book = exampleBook,
            AppUserId = _appUsers[0].Id,
            AppUser = _appUsers[0],
            LentAt = DateTime.Parse("06/04/2023"),
            ReturnAt = DateTime.Parse("04/05/2023")
        };
        await testLibraryRepository.Add(mariLends);
        var repoEnumerable = await testLibraryRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.NotEmpty(repoList);
        Assert.Single(repoList);
        var firstBookLentOut = repoList[0];
        Assert.Equal(exampleBook.Id, firstBookLentOut.BookId);
        Assert.NotNull(firstBookLentOut.Book);
        Assert.Equal("Süiten für Violoncello solo, BWV 1007-1012", firstBookLentOut.Book.Title);
        Assert.Equal("Johann Sebastian Bach", firstBookLentOut.Book.Authors);
    }

    [Fact]
    public async void TestFindLentOutBookFromRepo()
    {
        var testLibraryRepository = new BookLentOutRepository(_ctx);
        var exampleBook = _exampleBooks[1];
        var juhanLends = new BookLentOut
        {
            Id = Guid.NewGuid(),
            BookId = exampleBook.Id,
            Book = exampleBook,
            AppUserId = _appUsers[1].Id,
            AppUser = _appUsers[1],
            LentAt = DateTime.Parse("13/03/2023"),
            ReturnAt = DateTime.Parse("10/04/2023")
        };
        await testLibraryRepository.Add(juhanLends);
        var foundLentOutBook = await testLibraryRepository.Find(juhanLends.Id);
        Assert.NotNull(foundLentOutBook);
        Assert.Equal(exampleBook.Id, foundLentOutBook.BookId);
        Assert.Equal(DateTime.Parse("13/03/2023"), foundLentOutBook.LentAt);
    }

    [Fact]
    public async void TestAddThenRemoveLentOutBook()
    {
        var testLibraryRepository = new BookLentOutRepository(_ctx);
        var exampleBook = _exampleBooks[3];
        var mariLends = new BookLentOut
        {
            Id = Guid.NewGuid(),
            BookId = exampleBook.Id,
            Book = exampleBook,
            AppUserId = _appUsers[0].Id,
            AppUser = _appUsers[0],
            LentAt = DateTime.Parse("24/03/2021"),
            ReturnAt = DateTime.Parse("21/03/2021")
        };
        await testLibraryRepository.Add(mariLends);
        await testLibraryRepository.Remove(mariLends);
        var repoEnumerable = await testLibraryRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
    }

    [Fact]
    public async void TestAddThenRemoveLentOutBookById()
    {
        var testLibraryRepository = new BookLentOutRepository(_ctx);
        var exampleBook = _exampleBooks[0];
        var mariLends = new BookLentOut
        {
            Id = Guid.NewGuid(),
            BookId = exampleBook.Id,
            Book = exampleBook,
            AppUserId = _appUsers[0].Id,
            AppUser = _appUsers[0],
            LentAt = DateTime.Parse("24/03/2021"),
            ReturnAt = DateTime.Parse("21/03/2021")
        };
        await testLibraryRepository.Add(mariLends);
        await testLibraryRepository.RemoveById(mariLends.Id);
        var repoEnumerable = await testLibraryRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
    }

    [Fact]
    public async void TestAddThenUpdateLentOutBook()
    {
        var testLibraryRepository = new BookLentOutRepository(_ctx);
        var exampleBook = _exampleBooks[4];
        var juhanLends = new BookLentOut
        {
            Id = Guid.NewGuid(),
            BookId = exampleBook.Id,
            Book = exampleBook,
            AppUserId = _appUsers[1].Id,
            AppUser = _appUsers[1],
            LentAt = DateTime.Parse("17/04/2023"),
            ReturnAt = DateTime.Parse("13/05/2023")
        };
        await testLibraryRepository.Add(juhanLends);
        juhanLends.ReturnAt = juhanLends.ReturnAt.AddDays(28);
        await testLibraryRepository.Update(juhanLends);
        var repoEnumerable = await testLibraryRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        var bookLentOutFound = await testLibraryRepository.Find(juhanLends.Id);
        Assert.Equal(DateTime.Parse("10/06/2023"), bookLentOutFound!.ReturnAt);
    }

    [Fact]
    public async void TestAddThenUpdateLentOutBookById()
    {
        var testLibraryRepository = new BookLentOutRepository(_ctx);
        var exampleBook = _exampleBooks[4];
        var juhanLends = new BookLentOut
        {
            Id = Guid.NewGuid(),
            BookId = exampleBook.Id,
            Book = exampleBook,
            AppUserId = _appUsers[1].Id,
            AppUser = _appUsers[1],
            LentAt = DateTime.Parse("17/04/2023"),
            ReturnAt = DateTime.Parse("13/05/2023")
        };
        await testLibraryRepository.Add(juhanLends);
        juhanLends.ReturnAt = juhanLends.ReturnAt.AddDays(28);
        await testLibraryRepository.UpdateById(juhanLends.Id);
        var repoEnumerable = await testLibraryRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        var bookLentOutFound = await testLibraryRepository.Find(juhanLends.Id);
        Assert.Equal(DateTime.Parse("10/06/2023"), bookLentOutFound!.ReturnAt);
    }

    public void Dispose()
    {
        _ctx.Dispose();
        GC.SuppressFinalize(this);
    }

}