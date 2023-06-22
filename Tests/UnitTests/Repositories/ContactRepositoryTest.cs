using DAL;
using DAL.Repositories;
using DAL.Repositories.Contacts;
using Domain;
using Domain.Contacts;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace Tests.UnitTests.Repositories;

public class ContactRepositoryTest: IDisposable
{
    private readonly AppDbContext _ctx;
    private readonly ContactType _contactTypePhone;
    private readonly ContactType _contactTypeMail;

    public ContactRepositoryTest()
    {
        // set up mock database - in-memory
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new AppDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        _contactTypePhone = new ContactType { Name = "Phone number" };
        _contactTypeMail = new ContactType { Name = "Email"};
    }

    [Fact]
    public async void TestContactRepositoryIsNotNullAndEmpty()
    {
        var testContactRepository = new ContactRepository(_ctx);
        var repoEnumerable = await testContactRepository.All();
        Assert.NotNull(repoEnumerable);
        Assert.Empty(repoEnumerable);
    }

    [Fact]
    public async void TestAddContactToRepo()
    {
        var testContactRepository = new ContactRepository(_ctx);
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var juhanPhone = new Contact
        {
            ContactTypeId = _contactTypePhone.Id,
            ContactType = _contactTypePhone,
            PersonId = juhan.Id,
            Person = juhan,
            Value = "+372 639 8197"
        };
        juhan.Contacts = new List<Contact> { juhanPhone };
        await testContactRepository.Add(juhanPhone);
        var repoEnumerable = await testContactRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.NotEmpty(repoList);
        Assert.Single(repoList);
        Assert.Equal("+372 639 8197", repoList[0].Value);
    }

    [Fact]
    public async void TestFindExampleDataFromRepo()
    {
        var testContactRepository = new ContactRepository(_ctx);
        var mari = new Person
        {
            FirstName = "Mari",
            LastName = "Metsla"
        };
        var mariPhone = new Contact
        {
            ContactTypeId = _contactTypePhone.Id,
            ContactType = _contactTypePhone,
            PersonId = mari.Id,
            Person = mari,
            Value = "+372 632 3523"
        };
        mari.Contacts = new List<Contact> { mariPhone };
        await testContactRepository.Add(mariPhone);
        var foundContact = await testContactRepository.Find(mariPhone.Id);
        Assert.NotNull(foundContact);
        Assert.Equal("+372 632 3523", foundContact.Value);
    }

    [Fact]
    public async void TestAddOneThenRemoveContact()
    {
        var testContactRepository = new ContactRepository(_ctx);
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var juhanPhone = new Contact
        {
            ContactTypeId = _contactTypePhone.Id,
            ContactType = _contactTypePhone,
            PersonId = juhan.Id,
            Person = juhan,
            Value = "+372 639 8197"
        };
        juhan.Contacts = new List<Contact> { juhanPhone };
        await testContactRepository.Add(juhanPhone);
        await testContactRepository.Remove(juhanPhone);
        var repoEnumerable = await testContactRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Empty(repoList);
    }

    [Fact]
    public async void TestAddTwoThenRemoveOneContactById()
    {
        var testContactRepository = new ContactRepository(_ctx);
        var madis = new Person
        {
            FirstName = "Madis",
            LastName = "Kaljuste"
        };
        var madisPhone = new Contact
        {
            ContactTypeId = _contactTypePhone.Id,
            ContactType = _contactTypePhone,
            PersonId = madis.Id,
            Person = madis,
            Value = "+372 534 2304"
        };
        var ingrid = new Person
        {
            FirstName = "Ingrid",
            LastName = "Mauer"
        };
        var ingridMail = new Contact
        {
            ContactTypeId = _contactTypeMail.Id,
            ContactType = _contactTypeMail,
            PersonId = ingrid.Id,
            Person = ingrid,
            Value = "ingrid.mauer@muba.edu.ee"
        };
        await testContactRepository.Add(madisPhone);
        await testContactRepository.Add(ingridMail);
        await testContactRepository.RemoveById(madisPhone.Id);
        var repoEnumerable = await testContactRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        Assert.Equal("ingrid.mauer@muba.edu.ee", repoList[0].Value);
    }

    [Fact]
    public async void TestUpdateContact()
    {
        var testContactRepository = new ContactRepository(_ctx);
        var juhan = new Person
        {
            FirstName = "Juhan",
            LastName = "Kalju"
        };
        var juhanPhone = new Contact
        {
            ContactTypeId = _contactTypePhone.Id,
            ContactType = _contactTypePhone,
            PersonId = juhan.Id,
            Person = juhan,
            Value = "+372 639 8197"
        };
        juhan.Contacts = new List<Contact> { juhanPhone };
        await testContactRepository.Add(juhanPhone);
        juhanPhone.Value = "+372 598 2039";
        await testContactRepository.Update(juhanPhone);
        var repoEnumerable = await testContactRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        Assert.Equal("+372 598 2039", repoList[0].Value);
    }

    [Fact]
    public async void TestUpdateContactById()
    {
        var testContactRepository = new ContactRepository(_ctx);
        var ingrid = new Person
        {
            FirstName = "Ingrid",
            LastName = "Mauer"
        };
        var ingridMail = new Contact
        {
            ContactTypeId = _contactTypeMail.Id,
            ContactType = _contactTypeMail,
            PersonId = ingrid.Id,
            Person = ingrid,
            Value = "ingridmauer@muba.edu.ee"
        };
        await testContactRepository.Add(ingridMail);
        ingridMail.Value = "ingrid.mauer@muba.edu.ee";
        await testContactRepository.UpdateById(ingridMail.Id);
        var repoEnumerable = await testContactRepository.All();
        var repoList = repoEnumerable.ToList();
        Assert.Single(repoList);
        Assert.Equal("ingrid.mauer@muba.edu.ee", repoList[0].Value);
    }
    
    public void Dispose()
    {
        _ctx.Dispose();
        GC.SuppressFinalize(this);
    }
}
