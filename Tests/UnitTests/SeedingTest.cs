using DAL;
using DAL.Seeding;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Tests;

public class SeedingTest : IDisposable
{
    private readonly AppDbContext _ctx;
    
    public SeedingTest()
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
    public void TestSeedIdentity()
    {
        
    }

    [Fact]
    public async void TestSeedData()
    {
        await AppDataInit.SeedAppData(_ctx);
        Assert.Equal(3, await _ctx.InstitutionType.CountAsync());
        Assert.Equal(2, await _ctx.ContactType.CountAsync());
        Assert.Equal(1, await _ctx.Institution.CountAsync());
    }
    
    public void Dispose()
    {
        _ctx.Dispose();
        GC.SuppressFinalize(this);
    }
}