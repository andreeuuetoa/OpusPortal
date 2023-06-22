using DAL;
using DAL.Repositories.Studying_logic;
using Domain.Identity;
using Domain.Studying_logic;
using Microsoft.EntityFrameworkCore;

namespace Tests.UnitTests.Repositories;

public class PersonOnCurriculumRepositoryTest : IDisposable
{
    private readonly AppDbContext _ctx;

    public PersonOnCurriculumRepositoryTest()
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
    public async void TestCurriculumRepositoryIsNotNullAndEmpty()
    {
        var testCurriculumRepository = new PersonOnCurriculumRepository(_ctx);
        var repoEnumerable = await testCurriculumRepository.All();
        Assert.NotNull(repoEnumerable);
        Assert.Empty(repoEnumerable);
    }

    [Fact]
    public async void TestAddCurriculumToRepo()
    {
        var testCurriculumRepository = new PersonOnCurriculumRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var studentRole = new AppRole
        {
            Name = "Student"
        };
        var mariUserFrom = DateTime.Parse("01/09/2015");
        var mariUserUntil = DateTime.Parse("21/06/2021");
        var mariUser = CreateUser(mari, studentRole, mariUserFrom, mariUserUntil);
        
        var violinMajor = new Curriculum
        {
            Name = "Violin major",
            CurriculumCode = "KMV11/23",
            From = DateTime.Parse("15/07/2023"),
            Until = DateTime.Parse("15/07/2025")
        };
        var mariInViolinMajorFrom = DateTime.Parse("23/08/2023");
        var mariInViolinMajorUntil = DateTime.Parse("23/06/2026");
        var mariInViolinMajor = AssignPersonToCurriculum(
            mariUser, violinMajor, mariInViolinMajorFrom, mariInViolinMajorUntil
        );
        await testCurriculumRepository.Add(mariInViolinMajor);
        var repoEnumerable = await testCurriculumRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        Assert.Equal("Violin major", repoList[0].Curriculum!.Name);
    }

    [Fact]
    public async void TestFindExampleDataFromRepo()
    {
        var testCurriculumRepository = new PersonOnCurriculumRepository(_ctx);
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var studentRole = new AppRole
        {
            Name = "Student"
        };
        var juhanUserFrom = DateTime.Parse("01/09/2016");
        var juhanUserUntil = DateTime.Parse("21/06/2023");
        var juhanUser = CreateUser(juhan, studentRole, juhanUserFrom, juhanUserUntil);
        
        var pianoMajor = new Curriculum
        {
            Name = "Piano major",
            CurriculumCode = "KMP11/23",
            From = DateTime.Parse("15/07/2023"),
            Until = DateTime.Parse("15/07/2025")
        };
        var juhanInPianoMajorFrom = DateTime.Parse("20/08/2023");
        var juhanInPianoMajorUntil = DateTime.Parse("23/06/2026");
        var juhanInPianoMajor = AssignPersonToCurriculum(
            juhanUser, pianoMajor, juhanInPianoMajorFrom, juhanInPianoMajorUntil
        );
        await testCurriculumRepository.Add(juhanInPianoMajor);
        var personOnCurriculumFound = await testCurriculumRepository.Find(juhanInPianoMajor.Id);
        Assert.Equal(juhanInPianoMajor.Id, personOnCurriculumFound!.Id);
        Assert.Equal("Juhan", personOnCurriculumFound.AppUser!.Person!.FirstName);
    }

    [Fact]
    public async void TestAddOneThenRemoveOnePersonOnCurriculum()
    {
        var testCurriculumRepository = new PersonOnCurriculumRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var studentRole = new AppRole
        {
            Name = "Student"
        };
        var mariUserFrom = DateTime.Parse("01/09/2015");
        var mariUserUntil = DateTime.Parse("21/06/2021");
        var mariUser = CreateUser(mari, studentRole, mariUserFrom, mariUserUntil);
        
        var fluteMajor = new Curriculum { 
            Name = "Flute major",
            CurriculumCode = "KMF11/23",
            From = DateTime.Parse("15/07/2023"),
            Until = DateTime.Parse("15/07/2025")
        };
        var mariInFluteMajorFrom = DateTime.Parse("19/08/2023");
        var mariInFluteMajorUntil = DateTime.Parse("23/06/2026");
        var mariInFluteMajor = AssignPersonToCurriculum(
            mariUser, fluteMajor, mariInFluteMajorFrom, mariInFluteMajorUntil
        );
        await testCurriculumRepository.Add(mariInFluteMajor);
        await testCurriculumRepository.Remove(mariInFluteMajor);
        var repoEnumerable = await testCurriculumRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
    }

    [Fact]
    public async void TestAddOneThenRemoveOnePersonOnCurriculumById()
    {
        var testCurriculumRepository = new PersonOnCurriculumRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var studentRole = new AppRole
        {
            Name = "Student"
        };
        var mariUserFrom = DateTime.Parse("01/09/2015");
        var mariUserUntil = DateTime.Parse("21/06/2021");
        var mariUser = CreateUser(mari, studentRole, mariUserFrom, mariUserUntil);
        
        var fluteMajor = new Curriculum { 
            Name = "Flute major",
            CurriculumCode = "KMF11/23",
            From = DateTime.Parse("15/07/2023"),
            Until = DateTime.Parse("15/07/2025")
        };
        var mariInFluteMajorFrom = DateTime.Parse("19/08/2023");
        var mariInFluteMajorUntil = DateTime.Parse("23/06/2026");
        var mariInFluteMajor = AssignPersonToCurriculum(
            mariUser, fluteMajor, mariInFluteMajorFrom, mariInFluteMajorUntil
        );
        await testCurriculumRepository.Add(mariInFluteMajor);
        await testCurriculumRepository.RemoveById(mariInFluteMajor.Id);
        var repoEnumerable = await testCurriculumRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
    }

    [Fact]
    public async void TestUpdatePersonOnCurriculum()
    {
        var testCurriculumRepository = new PersonOnCurriculumRepository(_ctx);
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var studentRole = new AppRole
        {
            Name = "Student"
        };
        var juhanUserFrom = DateTime.Parse("01/09/2016");
        var juhanUserUntil = DateTime.Parse("21/06/2023");
        var juhanUser = CreateUser(juhan, studentRole, juhanUserFrom, juhanUserUntil);
        
        var violinMajor = new Curriculum
        {
            Name = "Violin major",
            CurriculumCode = "KMV11/23",
            From = DateTime.Parse("15/07/2023"),
            Until = DateTime.Parse("15/07/2025")
        };
        var juhanInViolinMajorFrom = DateTime.Parse("15/08/2023");
        var juhanInViolinMajorUntil = DateTime.Parse("23/06/2026");
        var juhanInViolinMajor = AssignPersonToCurriculum(
            juhanUser, violinMajor, juhanInViolinMajorFrom, juhanInViolinMajorUntil
        );
        await testCurriculumRepository.Add(juhanInViolinMajor);
        juhanInViolinMajor.Until = DateTime.Parse("27/07/2024");
        await testCurriculumRepository.Update(juhanInViolinMajor);
        var repoEnumerable = await testCurriculumRepository.All();
        var repoList = repoEnumerable.ToList();
        var juhanInList = repoList[0];
        Assert.NotNull(juhanInList);
        Assert.Equal(juhanInViolinMajor.Id, juhanInList.Id);
        Assert.Equal(DateTime.Parse("27/07/2024"), juhanInList.Until);
    }

    [Fact]
    public async void TestUpdatePersonOnCurriculumById()
    {
        var testCurriculumRepository = new PersonOnCurriculumRepository(_ctx);
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var studentRole = new AppRole
        {
            Name = "Student"
        };
        var juhanUserFrom = DateTime.Parse("01/09/2016");
        var juhanUserUntil = DateTime.Parse("21/06/2023");
        var juhanUser = CreateUser(juhan, studentRole, juhanUserFrom, juhanUserUntil);
        
        var violinMajor = new Curriculum
        {
            Name = "Violin major",
            CurriculumCode = "KMV11/23",
            From = DateTime.Parse("15/07/2023"),
            Until = DateTime.Parse("15/07/2025")
        };
        var juhanInViolinMajorFrom = DateTime.Parse("15/08/2023");
        var juhanInViolinMajorUntil = DateTime.Parse("23/06/2026");
        var juhanInViolinMajor = AssignPersonToCurriculum(
            juhanUser, violinMajor, juhanInViolinMajorFrom, juhanInViolinMajorUntil
        );
        await testCurriculumRepository.Add(juhanInViolinMajor);
        juhanInViolinMajor.Until = DateTime.Parse("27/07/2024");
        await testCurriculumRepository.UpdateById(juhanInViolinMajor.Id);
        var repoEnumerable = await testCurriculumRepository.All();
        var repoList = repoEnumerable.ToList();
        var juhanInList = repoList[0];
        Assert.NotNull(juhanInList);
        Assert.Equal(juhanInViolinMajor.Id, juhanInList.Id);
        Assert.Equal(DateTime.Parse("27/07/2024"), juhanInList.Until);
    }

    private static AppUser CreateUser(
        Person person, AppRole userRole, DateTime from, DateTime until
    )
    {
        return new AppUser
        {
            PersonId = person.Id,
            Person = person,
            AppRoleId = userRole.Id,
            AppRole = userRole,
            From = from,
            Until = until
        };
    }

    private static PersonOnCurriculum AssignPersonToCurriculum(
        AppUser studentUser, Curriculum curriculum, DateTime from, DateTime until
    )
    {
        return new PersonOnCurriculum
        {
            AppUserId = studentUser.Id,
            AppUser = studentUser,
            CurriculumId = curriculum.Id,
            Curriculum = curriculum,
            From = from,
            Until = until
        };
    }

    public void Dispose()
    {
        _ctx.Dispose();
        GC.SuppressFinalize(this);
    }
}