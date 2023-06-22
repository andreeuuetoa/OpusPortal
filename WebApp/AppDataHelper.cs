using DAL;
using DAL.Seeding;
using Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebApp;

/// <summary>
/// A class for creating initial application data.
/// </summary>
public static class AppDataHelper
{
    /// <summary>
    /// Create initial data to be stored in the database - admin user, user roles, MUBA institution data.
    /// Create a new migration and apply it to the database, if needed.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ApplicationException"></exception>
    public static async Task SetupAppData(IApplicationBuilder app, IConfiguration configuration)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        await using var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        if (context == null) throw new ApplicationException("Problem in service. Can't initialize the DB Context.");

        using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
        if (userManager == null) throw new ApplicationException("Problem in service. Can't initialize UserManager.");

        using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();
        if (roleManager == null) throw new ApplicationException("Problem in service. Can't initialize RoleManager.");
    
        var logger = serviceScope.ServiceProvider.GetService<ILogger<IApplicationBuilder>>();
        if (logger == null) throw new ApplicationException("Problem in service. Can't initialize the logger.");

        if (context.Database.ProviderName!.Contains("InMemory")) return;
        
        if (configuration.GetValue<bool>("DataInit:MigrateDatabase"))
        {
            logger.LogInformation("Migrating database");
            await AppDataInit.MigrateDatabase(context);
        }
    
        if (configuration.GetValue<bool>("DataInit:SeedData"))
        {
            logger.LogInformation("Seeding data");
            await AppDataInit.SeedAppData(context);
        }
    
        if (configuration.GetValue<bool>("DataInit:SeedIdentity"))
        {
            logger.LogInformation("Seeding identity");
            await AppDataInit.SeedIdentity(userManager, roleManager);
        }
    }
}