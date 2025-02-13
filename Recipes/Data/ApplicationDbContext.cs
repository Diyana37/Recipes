using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Recipes.Data.Entities;
using Recipes.Data.Entities.Identity;
using System.Reflection.Emit;

namespace Recipes.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public DbSet<RecipeNationality> RecipeNationalities { get; set; }

        public DbSet<RecipeType> RecipeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Recipe>()
            .ToTable(r => r.HasCheckConstraint("CHK_Recipe_PreparationTime", "[PreparationTime] BETWEEN 0 AND 1000"));

            builder.Entity<Recipe>()
            .ToTable(r => r.HasCheckConstraint("CHK_Recipe_CookingTime", "[CookingTime] BETWEEN 0 AND 1000"));

            builder.Entity<Recipe>()
            .ToTable(r => r.HasCheckConstraint("CHK_Recipe_Portions", "[Portions] BETWEEN 1 AND 100"));

            builder.Entity<Recipe>()
            .ToTable(r => r.HasCheckConstraint("CHK_Recipe_Difficulty", "[Difficulty] BETWEEN 1 AND 10"));
        }
    }
}
