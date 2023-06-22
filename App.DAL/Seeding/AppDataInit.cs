using Domain.Contacts;
using Domain.Identity;
using Domain.Institutions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.Seeding;

public static class AppDataInit
{
    public static async Task MigrateDatabase(AppDbContext context)
    {
        await context.Database.MigrateAsync();
    }
    
    public static async Task SeedAppData(AppDbContext context)
    {
        await CreateInstitutionTypes(context);
        await CreateSchool(context);
        await CreateContactTypes(context);
    }

    private static async Task CreateInstitutionTypes(AppDbContext context)
    {
        if (context.InstitutionType.Any()) return;
        var institutionTypes = new InstitutionType[]
        {
            new()
            {
                Name = "School"
            },
            new()
            {
                Name = "Venue"
            },
            new()
            {
                Name = "Other"
            }
        };
        await context.InstitutionType.AddRangeAsync(institutionTypes);
        await context.SaveChangesAsync();
    }

    private static async Task CreateSchool(AppDbContext context)
    {
        var schoolType = context.InstitutionType.Where(x => x.Name.Equals("School")).ToList()[0];
        await context.Institution.AddAsync(
            new Institution
            {
                Id = Guid.NewGuid(),
                InstitutionTypeId = schoolType.Id,
                InstitutionType = schoolType,
                Name = "Tallinna Muusika- ja Balletikool",
                RegistryCode = "77001576",
                Address = "Pärnu mnt 59, Tallinn 10135",
                EstablishedAt = DateTime.SpecifyKind(DateTime.Parse("01/01/2021"), DateTimeKind.Utc),
                FinishedAt = null
            }
        );
        await context.SaveChangesAsync();
    }

    private static async Task CreateContactTypes(AppDbContext context)
    {
        if (context.ContactType.Any()) return;
        var contactTypes = new ContactType[]
        {
            new()
            {
                Name = "Email"
                
            },
            new()
            {
                Name = "Phone"
            }
        };
        await context.ContactType.AddRangeAsync(contactTypes);
        await context.SaveChangesAsync();
    }

    public static async Task SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        await CreateRoles(roleManager);
        await CreateAdmin(userManager, roleManager);
    }

    private static async Task CreateRoles(RoleManager<AppRole> roleManager)
    {
        var roles = new[]
        {
            "Student",
            "Teacher",
            "Admin",
            "Other"
        };
        foreach (var roleName in roles)
        {
            var existingRole = await roleManager.FindByNameAsync(roleName);
            if (existingRole != null)
            {
                continue;
            }
            var identityResult = await roleManager.CreateAsync(new AppRole
            {
                Name = roleName
            });
            if (!identityResult.Succeeded)
            {
                throw new ApplicationException("Role creation failed");
            }
        }
    }

    private static async Task CreateAdmin(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        (Guid id, string email, string userName, string password) userData = (Guid.NewGuid(), "admin@opusportal.com", "Admin", "Foo.bar1");
        var user = await userManager.FindByEmailAsync(userData.email);
        if (user != null) return;
        
        var adminRole = await roleManager.FindByNameAsync("Admin");
        user = new AppUser
        {
            Id = userData.id,
            Email = userData.email,
            EmailConfirmed = true,
            UserName = userData.userName,
            AppRoleId = adminRole!.Id,
            AppRole = adminRole,
            From = DateTime.UtcNow
        };
        var result = await userManager.CreateAsync(user, userData.password);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Cannot seed admin user.");
        }
    }
}
