using AutoMapper;
using DAL;
using DAL.Repositories;
using DAL.Repositories.Concerts;
using Domain.Concerts;
using Domain.Identity;
using Domain.Institutions;
using Microsoft.EntityFrameworkCore;

namespace Tests.UnitTests.Repositories;

public class PerformanceRepositoryTest : IDisposable
{
    private readonly AppDbContext _ctx;
    private PerformanceRepository? _testPerformanceRepository;

    public PerformanceRepositoryTest()
    {
        // set up mock database - in-memory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
    }
    
    [Fact]
    public async void TestPerformanceRepositoryIsNotNullAndEmpty()
    {
        _testPerformanceRepository = new PerformanceRepository(_ctx);
        var repoEnumerable = await _testPerformanceRepository.All();
        Assert.NotNull(repoEnumerable);
        Assert.Empty(repoEnumerable);
    }

    [Fact]
    public async void TestAddPersonAtConcertToRepo()
    {
        _testPerformanceRepository = new PerformanceRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var venueType = new InstitutionType
        {
            Name = "Venue"
        };
        var exampleVenue = new Institution
        {
            InstitutionTypeId = venueType.Id,
            InstitutionType = venueType,
            Name = "FooBar",
            RegistryCode = "12345678",
            Address = "FizzBarr 12, Qwerty",
            EstablishedAt = DateTime.Parse("01/01/1997")
        };
        var exampleConcert = new Concert
        {
            InstitutionId = exampleVenue.Id,
            Institution = exampleVenue,
            Name = "Example concert",
            From = DateTime.Parse("21/04/2023 14:00:00"),
            Until = DateTime.Parse("21/04/2023 16:00:00")
        };
        var mariAtConcert = new PersonAtConcert
        {
            PersonId = mari.Id,
            Person = mari,
            ConcertId = exampleConcert.Id,
            Concert = exampleConcert,
            From = exampleConcert.From,
            Until = exampleConcert.Until
        };
        await _testPerformanceRepository.Add(mariAtConcert);
        var repoEnumerable = await _testPerformanceRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.NotEmpty(repoList);
        Assert.Single(repoList);
        Assert.Equal("Example concert", repoList[0].Concert!.Name);
    }

    [Fact]
    public async void TestFindExampleDataFromRepo()
    {
        _testPerformanceRepository = new PerformanceRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var venueType = new InstitutionType
        {
            Name = "Venue"
        };
        var exampleVenue = new Institution
        {
            InstitutionTypeId = venueType.Id,
            InstitutionType = venueType,
            Name = "FooBar",
            RegistryCode = "12345678",
            Address = "FizzBarr 12, Qwerty",
            EstablishedAt = DateTime.Parse("01/01/1997")
        };
        var exampleConcert = new Concert
        {
            InstitutionId = exampleVenue.Id,
            Institution = exampleVenue,
            Name = "Example concert",
            From = DateTime.Parse("21/04/2023 14:00:00"),
            Until = DateTime.Parse("21/04/2023 16:00:00")
        };
        var mariAtConcert = new PersonAtConcert
        {
            PersonId = mari.Id,
            Person = mari,
            ConcertId = exampleConcert.Id,
            Concert = exampleConcert,
            From = exampleConcert.From,
            Until = exampleConcert.Until
        };
        await _testPerformanceRepository.Add(mariAtConcert);
        var foundPersonAtConcert = await _testPerformanceRepository.Find(mariAtConcert.Id);
        Assert.Equal(mariAtConcert.Id, foundPersonAtConcert!.Id);
        Assert.Equal("Mari", foundPersonAtConcert.Person!.FirstName);
    }

    [Fact]
    public async void TestAddOneThenRemoveOnePersonAtConcert()
    {
        _testPerformanceRepository = new PerformanceRepository(_ctx);
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Vaik"
        };
        var venueType = new InstitutionType
        {
            Name = "Venue"
        };
        var exampleVenue = new Institution
        {
            InstitutionTypeId = venueType.Id,
            InstitutionType = venueType,
            Name = "FooBar",
            RegistryCode = "12345678",
            Address = "FizzBarr 12, Qwerty",
            EstablishedAt = DateTime.Parse("01/01/1997")
        };
        var exampleConcert = new Concert
        {
            InstitutionId = exampleVenue.Id,
            Institution = exampleVenue,
            Name = "Example concert",
            From = DateTime.Parse("21/04/2023 14:00:00"),
            Until = DateTime.Parse("21/04/2023 16:00:00")
        };
        var juhanAtConcert = new PersonAtConcert
        {
            PersonId = juhan.Id,
            Person = juhan,
            ConcertId = exampleConcert.Id,
            Concert = exampleConcert,
            From = exampleConcert.From,
            Until = exampleConcert.Until
        };
        await _testPerformanceRepository.Add(juhanAtConcert);
        await _testPerformanceRepository.Remove(juhanAtConcert);
        var repoEnumerable = await _testPerformanceRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
    }

    [Fact]
    public async void TestTryToRemoveFromEmptyRepo()
    {
        _testPerformanceRepository = new PerformanceRepository(_ctx);
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Vaik"
        };
        var venueType = new InstitutionType
        {
            Name = "Venue"
        };
        var exampleVenue = new Institution
        {
            InstitutionTypeId = venueType.Id,
            InstitutionType = venueType,
            Name = "FooBar",
            RegistryCode = "12345678",
            Address = "FizzBarr 12, Qwerty",
            EstablishedAt = DateTime.Parse("01/01/1997")
        };
        var exampleConcert = new Concert
        {
            InstitutionId = exampleVenue.Id,
            Institution = exampleVenue,
            Name = "Example concert",
            From = DateTime.Parse("21/04/2023 14:00:00"),
            Until = DateTime.Parse("21/04/2023 16:00:00")
        };
        var juhanAtConcert = new PersonAtConcert
        {
            PersonId = juhan.Id,
            Person = juhan,
            ConcertId = exampleConcert.Id,
            Concert = exampleConcert,
            From = exampleConcert.From,
            Until = exampleConcert.Until
        };
        try
        {
            await _testPerformanceRepository.Remove(juhanAtConcert);
            Assert.Fail(
                "The test should fail, because an entity must not be able to be removed from an empty repository."
            );
        }
        catch (Exception)
        {
            // ignored
        }
    }
    
    [Fact]
    public async void TestAddOneThenRemoveOnePersonAtConcertById()
    {
        _testPerformanceRepository = new PerformanceRepository(_ctx);
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Vaik"
        };
        var venueType = new InstitutionType
        {
            Name = "Venue"
        };
        var exampleVenue = new Institution
        {
            InstitutionTypeId = venueType.Id,
            InstitutionType = venueType,
            Name = "FooBar",
            RegistryCode = "12345678",
            Address = "FizzBarr 12, Qwerty",
            EstablishedAt = DateTime.Parse("01/01/1997")
        };
        var exampleConcert = new Concert
        {
            InstitutionId = exampleVenue.Id,
            Institution = exampleVenue,
            Name = "Example concert",
            From = DateTime.Parse("21/04/2023 14:00:00"),
            Until = DateTime.Parse("21/04/2023 16:00:00")
        };
        var juhanAtConcert = new PersonAtConcert
        {
            PersonId = juhan.Id,
            Person = juhan,
            ConcertId = exampleConcert.Id,
            Concert = exampleConcert,
            From = exampleConcert.From,
            Until = exampleConcert.Until
        };
        await _testPerformanceRepository.Add(juhanAtConcert);
        await _testPerformanceRepository.RemoveById(juhanAtConcert.Id);
        var repoEnumerable = await _testPerformanceRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
    }

    [Fact]
    public async void TestTryToRemoveFromEmptyRepoById()
    {
        _testPerformanceRepository = new PerformanceRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var venueType = new InstitutionType
        {
            Name = "Venue"
        };
        var exampleVenue = new Institution
        {
            InstitutionTypeId = venueType.Id,
            InstitutionType = venueType,
            Name = "FooBar",
            RegistryCode = "12345678",
            Address = "FizzBarr 12, Qwerty",
            EstablishedAt = DateTime.Parse("01/01/1997")
        };
        var exampleConcert = new Concert
        {
            InstitutionId = exampleVenue.Id,
            Institution = exampleVenue,
            Name = "Example concert",
            From = DateTime.Parse("21/04/2023 14:00:00"),
            Until = DateTime.Parse("21/04/2023 16:00:00")
        };
        var mariAtConcert = new PersonAtConcert
        {
            PersonId = mari.Id,
            Person = mari,
            ConcertId = exampleConcert.Id,
            Concert = exampleConcert,
            From = exampleConcert.From,
            Until = exampleConcert.Until
        };
        try
        {
            await _testPerformanceRepository.RemoveById(mariAtConcert.Id);
            Assert.Fail(
                "The test should fail, because an entity must not be able to be removed from an empty repository."
            );
        }
        catch (Exception)
        {
            // ignored
        }
    }


    [Fact]
    public async void TestUpdatePersonAtConcert()
    {
        _testPerformanceRepository = new PerformanceRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var venueType = new InstitutionType
        {
            Name = "Venue"
        };
        var exampleVenue = new Institution
        {
            InstitutionTypeId = venueType.Id,
            InstitutionType = venueType,
            Name = "FooBar",
            RegistryCode = "12345678",
            Address = "FizzBarr 12, Qwerty",
            EstablishedAt = DateTime.Parse("01/01/1997")
        };
        var exampleConcert = new Concert
        {
            InstitutionId = exampleVenue.Id,
            Institution = exampleVenue,
            Name = "Example concert",
            From = DateTime.Parse("21/04/2023 14:00:00"),
            Until = DateTime.Parse("21/04/2023 16:00:00")
        };
        var mariAtConcert = new PersonAtConcert
        {
            PersonId = mari.Id,
            Person = mari,
            ConcertId = exampleConcert.Id,
            Concert = exampleConcert,
            From = exampleConcert.From,
            Until = exampleConcert.Until
        };
        await _testPerformanceRepository.Add(mariAtConcert);
        mariAtConcert.Until = DateTime.Parse("21/04/2023 16:00:00");
        await _testPerformanceRepository.Update(mariAtConcert);
        var repoEnumerable = await _testPerformanceRepository.All();
        var repoList = repoEnumerable.ToList();
        var mariInList = repoList[0];
        Assert.Equal("Mari", mariInList.Person!.FirstName);
        Assert.Equal(DateTime.Parse("21/04/2023 16:00:00"), mariInList.Until);
    }

    [Fact]
    public async void TestUpdatePersonAtConcertById()
    {
        _testPerformanceRepository = new PerformanceRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var venueType = new InstitutionType
        {
            Name = "Venue"
        };
        var exampleVenue = new Institution
        {
            InstitutionTypeId = venueType.Id,
            InstitutionType = venueType,
            Name = "FooBar",
            RegistryCode = "12345678",
            Address = "FizzBarr 12, Qwerty",
            EstablishedAt = DateTime.Parse("01/01/1997")
        };
        var exampleConcert = new Concert
        {
            InstitutionId = exampleVenue.Id,
            Institution = exampleVenue,
            Name = "Example concert",
            From = DateTime.Parse("21/04/2023 14:00:00"),
            Until = DateTime.Parse("21/04/2023 16:00:00")
        };
        var mariAtConcert = new PersonAtConcert
        {
            PersonId = mari.Id,
            Person = mari,
            ConcertId = exampleConcert.Id,
            Concert = exampleConcert,
            From = exampleConcert.From,
            Until = exampleConcert.Until
        };
        await _testPerformanceRepository.Add(mariAtConcert);
        mariAtConcert.Until = DateTime.Parse("21/04/2023 16:00:00");
        await _testPerformanceRepository.UpdateById(mariAtConcert.Id);
        var repoEnumerable = await _testPerformanceRepository.All();
        var repoList = repoEnumerable.ToList();
        var mariInList = repoList[0];
        Assert.Equal("Mari", mariInList.Person!.FirstName);
        Assert.Equal(DateTime.Parse("21/04/2023 16:00:00"), mariInList.Until);
    }
    
    public void Dispose()
    {
        _ctx.Dispose();
        GC.SuppressFinalize(this);
    }
}