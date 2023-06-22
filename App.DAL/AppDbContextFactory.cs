using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace DAL;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    private static readonly ILoggerFactory MyLoggerFactory =
        LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Information)
                .AddConsole();
        });
    
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var options = optionsBuilder
            .UseLoggerFactory(MyLoggerFactory)
            .UseNpgsql("Host=localhost;Port=5445;Database=opusportal-db;User ID=postgres;Password=postgres")
            .Options;

        return new AppDbContext(options);
    }
}