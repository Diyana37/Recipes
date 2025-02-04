using Microsoft.AspNetCore.Identity;
using Recipes.Data.Entities.Identity;
using System.Reflection.Metadata;

namespace Recipes.Seeders
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                IdentityRole admin = new IdentityRole(Constants.ADMINISTRATOR_ROLE);
                IdentityRole user = new IdentityRole(Constants.USER_ROLE);

                await roleManager.CreateAsync(admin);
                await roleManager.CreateAsync(user);
            }

            IList<ApplicationUser> administratorUsers = await userManager.GetUsersInRoleAsync(Constants.ADMINISTRATOR_ROLE);
            if (administratorUsers.Count() <= 0)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    Email = "stefko@abv.bg",
                    UserName = "stefko@abv.bg",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(applicationUser, "Pa$$w0rd");
                await userManager.AddToRoleAsync(applicationUser, Constants.ADMINISTRATOR_ROLE);
            }
        }
    }
}
