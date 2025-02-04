using Microsoft.AspNetCore.Identity;

namespace Recipes.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Recipes = new HashSet<Recipe>();
        }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
