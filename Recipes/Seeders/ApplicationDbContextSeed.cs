using Microsoft.AspNetCore.Identity;
using System.Reflection.Metadata;

namespace Recipes.Seeders
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                IdentityRole admin = new IdentityRole(Constants.ADMINISTRATOR_ROLE);
                IdentityRole user = new IdentityRole(Constants.USER_ROLE);

                await roleManager.CreateAsync(admin);
                await roleManager.CreateAsync(user);
            }

            IList<IdentityUser> administratorUsers = await userManager.GetUsersInRoleAsync(Constants.ADMINISTRATOR_ROLE);
            if (administratorUsers.Count() <= 0)
            {
                IdentityUser identityUser = new IdentityUser()
                {
                    Email = "stefko@abv.bg",
                    UserName = "stefko@abv.bg"
                };

                await userManager.CreateAsync(identityUser, "Pa$$w0rd");
                await userManager.AddToRoleAsync(identityUser, Constants.ADMINISTRATOR_ROLE);
            }
        }
    }
}
