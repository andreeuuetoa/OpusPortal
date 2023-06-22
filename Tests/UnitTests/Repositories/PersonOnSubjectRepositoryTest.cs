using AutoMapper;
using DAL;
using DAL.Repositories;
using DAL.Repositories.Studying_logic;
using Domain.Identity;
using Domain.Studying_logic;
using Microsoft.EntityFrameworkCore;

namespace Tests.UnitTests.Repositories;

public class PersonOnSubjectRepositoryTest: IDisposable
{
    private readonly AppDbContext _ctx;
    private readonly AppRole _studentRole;
    private PersonOnSubjectRepository? _testSubjectRepository;

    public PersonOnSubjectRepositoryTest()
    {
        // set up mock database - in-memory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();
        
        _studentRole = new AppRole
        {
            Name = "Student"
        };
    }
    
    [Fact]
    public async void TestPersonOnSubjectRepositoryIsNotNullAndEmpty()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var repoEnumerable = await _testSubjectRepository.All();
        Assert.NotNull(repoEnumerable);
        Assert.Empty(repoEnumerable);
    }

    [Fact]
    public async void TestAddOneSubjectToRepo()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var exampleSubjectSolfege = new Subject
        {
            ECTS = 6,
            Name = "Solfége I",
            SubjectCode = "AAA11"
        };
        var mariUser = new AppUser
        {
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            PersonId = mari.Id,
            Person = mari,
            From = DateTime.Parse("01/09/2015"),
            Until = DateTime.Parse("21/06/2021")
        };
        var mariOnSolfege = new PersonOnSubject
        {
            AppUserId = mariUser.Id,
            AppUser = mariUser,
            SubjectId = exampleSubjectSolfege.Id,
            Subject = exampleSubjectSolfege,
            From = DateTime.Parse("04/09/2015"),
            Until = DateTime.Parse("12/01/2016")
        };
        await _testSubjectRepository.Add(mariOnSolfege);
        var repoEnumerable = await _testSubjectRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.NotEmpty(repoList);
        Assert.Single(repoList);
        Assert.Equal("Solfége I", repoList[0].Subject!.Name);
    }

    [Fact]
    public async void TestFindExampleDataFromRepo()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var juhan = new Person
        {
            
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var juhanUser = new AppUser
        {
            
            PersonId = juhan.Id,
            Person = juhan,
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            From = DateTime.Parse("01/09/2016"),
            Until = DateTime.Parse("21/06/2023")
        };
        var exampleSubjectPiano = new Subject
        {
            
            ECTS = 6,
            Name = "Piano I",
            SubjectCode = "ABC12"
        };
        var juhanOnPiano = new PersonOnSubject
        {
            AppUserId = juhanUser.Id,
            AppUser = juhanUser,
            SubjectId = exampleSubjectPiano.Id,
            Subject = exampleSubjectPiano,
            From = DateTime.Parse("03/09/2016"),
            Until = DateTime.Parse("13/01/2017")
        };
        await _testSubjectRepository.Add(juhanOnPiano);
        var foundPersonOnSubject = await _testSubjectRepository.Find(juhanOnPiano.Id);
        Assert.Equal(juhanOnPiano.Id, foundPersonOnSubject!.Id);
    }

    [Fact]
    public async void TestAddOneThenRemoveOnePersonOnSubjectFromRepo()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var mari = new Person
        {
            
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var exampleSubjectSolfege = new Subject
        {
            
            ECTS = 6,
            Name = "Solfége I",
            SubjectCode = "AAA11"
        };
        var mariUser = new AppUser
        {
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            PersonId = mari.Id,
            Person = mari,
            From = DateTime.Parse("01/09/2015"),
            Until = DateTime.Parse("21/06/2021")
        };
        var mariOnSolfege = new PersonOnSubject
        {
            AppUserId = mariUser.Id,
            AppUser = mariUser,
            SubjectId = exampleSubjectSolfege.Id,
            Subject = exampleSubjectSolfege,
            From = DateTime.Parse("04/09/2015"),
            Until = DateTime.Parse("12/01/2016")
        };
        await _testSubjectRepository.Add(mariOnSolfege);
        await _testSubjectRepository.Remove(mariOnSolfege);
        var repoEnumerable = await _testSubjectRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
    }

    [Fact]
    public async void TestAddTwoThenRemoveOnePersonOnSubjectFromRepo()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var mari = new Person
        {
            
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var exampleSubjectSolfege = new Subject
        {
            
            ECTS = 6,
            Name = "Solfége I",
            SubjectCode = "AAA11"
        };
        var mariUser = new AppUser
        {
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            PersonId = mari.Id,
            Person = mari,
            From = DateTime.Parse("01/09/2015"),
            Until = DateTime.Parse("21/06/2021")
        };
        var mariOnSolfege = new PersonOnSubject
        {
            AppUserId = mariUser.Id,
            AppUser = mariUser,
            SubjectId = exampleSubjectSolfege.Id,
            Subject = exampleSubjectSolfege,
            From = DateTime.Parse("04/09/2015"),
            Until = DateTime.Parse("12/01/2016")
        };
        var juhan = new Person
        {
            
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var juhanUser = new AppUser
        {
            
            PersonId = juhan.Id,
            Person = juhan,
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            From = DateTime.Parse("01/09/2016"),
            Until = DateTime.Parse("21/06/2023")
        };
        var exampleSubjectPiano = new Subject
        {
            
            ECTS = 6,
            Name = "Piano I",
            SubjectCode = "ABC12"
        };
        var juhanOnPiano = new PersonOnSubject
        {
            AppUserId = juhanUser.Id,
            AppUser = juhanUser,
            SubjectId = exampleSubjectPiano.Id,
            Subject = exampleSubjectPiano,
            From = DateTime.Parse("03/09/2016"),
            Until = DateTime.Parse("13/01/2017")
        };
        var secondPersonOnSubjectId = juhanOnPiano.Id;
        await _testSubjectRepository.Add(mariOnSolfege);
        await _testSubjectRepository.Add(juhanOnPiano);
        await _testSubjectRepository.Remove(mariOnSolfege);
        var repoEnumerable = await _testSubjectRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        var personOnSubjectFound = await _testSubjectRepository.Find(secondPersonOnSubjectId);
        Assert.Equal(secondPersonOnSubjectId, personOnSubjectFound!.Id);
        Assert.Equal("Piano I", personOnSubjectFound.Subject!.Name);
    }

    [Fact]
    public async void TestAddTwoThenRemoveOnePersonOnSubjectByIdFromRepo()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var mari = new Person
        {
            
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var exampleSubjectSolfege = new Subject
        {
            
            ECTS = 6,
            Name = "Solfége I",
            SubjectCode = "AAA11"
        };
        var mariUser = new AppUser
        {
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            PersonId = mari.Id,
            Person = mari,
            From = DateTime.Parse("01/09/2015"),
            Until = DateTime.Parse("21/06/2021")
        };
        var mariOnSolfege = new PersonOnSubject
        {
            AppUserId = mariUser.Id,
            AppUser = mariUser,
            SubjectId = exampleSubjectSolfege.Id,
            Subject = exampleSubjectSolfege,
            From = DateTime.Parse("04/09/2015"),
            Until = DateTime.Parse("12/01/2016")
        };
        var juhan = new Person
        {
            
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var juhanUser = new AppUser
        {
            
            PersonId = juhan.Id,
            Person = juhan,
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            From = DateTime.Parse("01/09/2016"),
            Until = DateTime.Parse("21/06/2023")
        };
        var exampleSubjectPiano = new Subject
        {
            
            ECTS = 6,
            Name = "Piano I",
            SubjectCode = "ABC12"
        };
        var juhanOnPiano = new PersonOnSubject
        {
            AppUserId = juhanUser.Id,
            AppUser = juhanUser,
            SubjectId = exampleSubjectPiano.Id,
            Subject = exampleSubjectPiano,
            From = DateTime.Parse("03/09/2016"),
            Until = DateTime.Parse("13/01/2017")
        };
        await _testSubjectRepository.Add(mariOnSolfege);
        await _testSubjectRepository.Add(juhanOnPiano);
        await _testSubjectRepository.RemoveById(mariOnSolfege.Id);
        var repoEnumerable = await _testSubjectRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        var personOnSubjectFound = await _testSubjectRepository.Find(juhanOnPiano.Id);
        Assert.Equal(juhanOnPiano.Id, personOnSubjectFound!.Id);
        Assert.Equal("Piano I", personOnSubjectFound.Subject!.Name);
    }

    [Fact]
    public async void TestAddThenRemovePersonOnSubject()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var exampleSubjectSolfege = new Subject
        {
            
            ECTS = 6,
            Name = "Solfége I",
            SubjectCode = "AAA11"
        };
        var juhan = new Person
        {
            
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var juhanUser = new AppUser
        {
            
            PersonId = juhan.Id,
            Person = juhan,
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            From = DateTime.Parse("01/09/2016"),
            Until = DateTime.Parse("21/06/2023")
        };
        var juhanOnSolfege = new PersonOnSubject
        {
            AppUserId = juhanUser.Id,
            AppUser = juhanUser,
            SubjectId = exampleSubjectSolfege.Id,
            Subject = exampleSubjectSolfege,
            From = DateTime.Parse("03/09/2016"),
            Until = DateTime.Parse("09/01/2017")
        };
        await _testSubjectRepository.Add(juhanOnSolfege);
        var removedPersonOnSubject = await _testSubjectRepository.Remove(juhanOnSolfege);
        var repoEnumerable = await _testSubjectRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
        Assert.Equal(juhanOnSolfege.Id, removedPersonOnSubject?.Id);
    }

    [Fact]
    public async void TestAddThenRemovePersonOnSubjectById()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var exampleSubjectSolfege = new Subject
        {
            
            ECTS = 6,
            Name = "Solfége I",
            SubjectCode = "AAA11"
        };
        var juhan = new Person
        {
            
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var juhanUser = new AppUser
        {
            
            PersonId = juhan.Id,
            Person = juhan,
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            From = DateTime.Parse("01/09/2016"),
            Until = DateTime.Parse("21/06/2023")
        };
        var juhanOnSolfege = new PersonOnSubject
        {
            AppUserId = juhanUser.Id,
            AppUser = juhanUser,
            SubjectId = exampleSubjectSolfege.Id,
            Subject = exampleSubjectSolfege,
            From = DateTime.Parse("03/09/2016"),
            Until = DateTime.Parse("09/01/2017")
        };
        await _testSubjectRepository.Add(juhanOnSolfege);
        var removedPersonOnSubject = await _testSubjectRepository.RemoveById(juhanOnSolfege.Id);
        var repoEnumerable = await _testSubjectRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
        Assert.Equal(juhanOnSolfege.Id, removedPersonOnSubject?.Id);
    }

    [Fact]
    public async void TestAddThenUpdatePersonOnSubject()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var mari = new Person
        {
            
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var exampleSubjectPiano = new Subject
        {
            
            ECTS = 6,
            Name = "Piano I",
            SubjectCode = "ABC12"
        };
        var mariUser = new AppUser
        {
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            PersonId = mari.Id,
            Person = mari,
            From = DateTime.Parse("01/09/2015"),
            Until = DateTime.Parse("21/06/2021")
        };
        var mariOnPiano = new PersonOnSubject
        {
            AppUserId = mariUser.Id,
            AppUser = mariUser,
            SubjectId = exampleSubjectPiano.Id,
            Subject = exampleSubjectPiano,
            From = DateTime.Parse("04/09/2015"),
            Until = DateTime.Parse("06/01/2016")
        };
        await _testSubjectRepository.Add(mariOnPiano);
        mariOnPiano.SetAverageGrade(5.0);
        await _testSubjectRepository.Update(mariOnPiano);
        var repoEnumerable = await _testSubjectRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        var personOnSubjectFound = await _testSubjectRepository.Find(mariOnPiano.Id);
        Assert.Equal(5.0, personOnSubjectFound?.AverageGrade);
    }
    
    [Fact]
    public async void TestAddThenUpdatePersonOnSubjectById()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var mari = new Person
        {
            
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var exampleSubjectPiano = new Subject
        {
            
            ECTS = 6,
            Name = "Piano I",
            SubjectCode = "ABC12"
        };
        var mariUser = new AppUser
        {
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            PersonId = mari.Id,
            Person = mari,
            From = DateTime.Parse("01/09/2015"),
            Until = DateTime.Parse("21/06/2021")
        };
        var mariOnPiano = new PersonOnSubject
        {
            AppUserId = mariUser.Id,
            AppUser = mariUser,
            SubjectId = exampleSubjectPiano.Id,
            Subject = exampleSubjectPiano,
            From = DateTime.Parse("04/09/2015"),
            Until = DateTime.Parse("06/01/2016")
        };
        await _testSubjectRepository.Add(mariOnPiano);
        mariOnPiano.SetAverageGrade(5.0);
        await _testSubjectRepository.UpdateById(mariOnPiano.Id);
        var repoEnumerable = await _testSubjectRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        var personOnSubjectFound = await _testSubjectRepository.Find(mariOnPiano.Id);
        Assert.Equal(5.0, personOnSubjectFound?.AverageGrade);
    }

    [Fact]
    public async void TestGetAllPersonOnSubjectEntitiesForSpecificPerson()
    {
        _testSubjectRepository = new PersonOnSubjectRepository(_ctx);
        var exampleSubjectSolfege = new Subject
        {
            
            ECTS = 6,
            Name = "Solfége I",
            SubjectCode = "AAA11"
        };
        var exampleSubjectPiano = new Subject
        {
            
            ECTS = 6,
            Name = "Piano I",
            SubjectCode = "ABC12"
        };
        var mari = new Person
        {
            
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var mariUser = new AppUser
        {
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            PersonId = mari.Id,
            Person = mari,
            From = DateTime.Parse("01/09/2015"),
            Until = DateTime.Parse("21/06/2021")
        };
        var mariOnPiano = new PersonOnSubject
        {
            AppUserId = mariUser.Id,
            AppUser = mariUser,
            SubjectId = exampleSubjectPiano.Id,
            Subject = exampleSubjectPiano,
            From = DateTime.Parse("04/09/2015"),
            Until = DateTime.Parse("06/01/2016")
        };
        var juhan = new Person
        {
            
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var juhanUser = new AppUser
        {
            
            PersonId = juhan.Id,
            Person = juhan,
            AppRoleId = _studentRole.Id,
            AppRole = _studentRole,
            From = DateTime.Parse("01/09/2016"),
            Until = DateTime.Parse("21/06/2023")
        };
        var juhanOnSolfege = new PersonOnSubject
        {
            AppUserId = juhanUser.Id,
            AppUser = juhanUser,
            SubjectId = exampleSubjectSolfege.Id,
            Subject = exampleSubjectSolfege,
            From = DateTime.Parse("03/09/2016"),
            Until = DateTime.Parse("09/01/2017")
        };
        var mariOnSolfege = new PersonOnSubject
        {
            AppUserId = mariUser.Id,
            AppUser = mariUser,
            SubjectId = exampleSubjectSolfege.Id,
            Subject = exampleSubjectSolfege,
            From = DateTime.Parse("04/09/2015"),
            Until = DateTime.Parse("12/01/2016")
        };
        await _testSubjectRepository.Add(mariOnSolfege);
        await _testSubjectRepository.Add(mariOnPiano);
        await _testSubjectRepository.Add(juhanOnSolfege);
        var mariSubjectsFound = await _testSubjectRepository.AllWithUserId(mariOnSolfege.AppUserId);
        var mariSubjectsFoundList = mariSubjectsFound.ToList();
        Assert.Equal(2, mariSubjectsFoundList.Count);
        var juhanSubjectsFound = await _testSubjectRepository.AllWithUserId(juhanOnSolfege.AppUserId);
        var juhanSubjectsFoundList = juhanSubjectsFound.ToList();
        Assert.Single(juhanSubjectsFoundList);
    }
    
    public void Dispose()
    {
        _ctx.Dispose();
        GC.SuppressFinalize(this);
    }
}