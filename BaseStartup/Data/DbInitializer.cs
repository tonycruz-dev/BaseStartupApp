using BaseStartup.Entities;
using Microsoft.AspNetCore.Identity;

namespace BaseStartup.Data;

public static class DbInitializer
{
    public static async Task Initialize(DataContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ILoggerFactory loggerFactory)
    {
        try
        {
            using (var tranaction = context.Database.BeginTransaction())
            {
                if (!roleManager.Roles.Any())
                {
                    var roles = new List<IdentityRole>
                    {
                        new IdentityRole {Name = "Member", },
                        new IdentityRole {Name = "Admin"},
                        new IdentityRole {Name = "Manager"},
                    };
                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(role);
                    }
                }
                if (!userManager.Users.Any())
                {
                    var user = new AppUser
                    {
                        UserName = "user@me.com",
                        Email = "user@me.com"
                    };

                    await userManager.CreateAsync(user, "Pa55w0rd!");
                    await userManager.AddToRoleAsync(user, "Member");

                    var admin = new AppUser
                    {
                        UserName = "admin",
                        Email = "admin@test.com"
                    };

                    await userManager.CreateAsync(admin, "Pa55w0rd!");
                    await userManager.AddToRolesAsync(admin, new[] { "Member", "Admin" });
                    await context.SaveChangesAsync();
                    tranaction.Commit();
                }

            }

        }
        catch (Exception)
        {

            throw;
        }
    }
}