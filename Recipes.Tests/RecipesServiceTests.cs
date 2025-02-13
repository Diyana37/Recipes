using Microsoft.EntityFrameworkCore;
using Recipes.Data.Entities;
using Recipes.Data;
using Recipes.InputModels.Recipes;
using Recipes.Interfaces;
using Recipes.Services;
using System.Text;


namespace Recipes.Tests
{
    public class RecipesServiceTests
    {
        private ApplicationDbContext dbContext;
        private IRecipesService recipesService;

        [SetUp]
        public async Task Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDB")
                .Options;

            this.dbContext = new ApplicationDbContext(options);

            // Add RecipeType
            var recipeType = new RecipeType { Name = "Vegetarian" };
            await this.dbContext.RecipeTypes.AddAsync(recipeType);
            await this.dbContext.SaveChangesAsync();

            // Add RecipeNationality
            var recipeNationality = new RecipeNationality { Name = "Italian" };
            await this.dbContext.RecipeNationalities.AddAsync(recipeNationality);
            await this.dbContext.SaveChangesAsync();

            // Add Category
            var category = new Category { Name = "Pasta" };
            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();

            this.recipesService = new RecipesService(this.dbContext, null); // Mock cloudinaryService if needed
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted(); // Reset database after each test
            this.dbContext.Dispose();
        }

        [Test]
        public async Task CreateAsync_Should_Add_Recipe_To_Database()
        {
            // Arrange
            var recipeType = await this.dbContext.RecipeTypes.FirstOrDefaultAsync();
            var recipeNationality = await this.dbContext.RecipeNationalities.FirstOrDefaultAsync();
            var category = await this.dbContext.Categories.FirstOrDefaultAsync();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Flour - 200g");
            sb.AppendLine("Sugar - 100g");
            sb.AppendLine("Cocoa - 50g");

            var createRecipeInput = new CreateRecipeInputModel
            {
                Name = "Chocolate Cake",
                Description = "A delicious chocolate cake",
                PreparationTime = 10,
                CookingTime = 30,
                Portions = 8,
                Difficulty = 5,
                RecipeTypeId = recipeType.Id,
                RecipeNationalityId = recipeNationality.Id,
                CategoryId = category.Id,
                Ingredients = sb.ToString()
            };

            var userId = "user123";

            // Act
            await this.recipesService.CreateAsync(createRecipeInput, userId);
            var actual = await this.dbContext.Recipes
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync();

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Name, Is.EqualTo(createRecipeInput.Name));
            Assert.That(actual.Description, Is.EqualTo(createRecipeInput.Description));
            Assert.That(actual.Ingredients.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task DeleteAsync_Should_Remove_Recipe_From_Database()
        {
            // Arrange
            var recipeType = await this.dbContext.RecipeTypes.FirstOrDefaultAsync();
            var recipeNationality = await this.dbContext.RecipeNationalities.FirstOrDefaultAsync();
            var category = await this.dbContext.Categories.FirstOrDefaultAsync();

            var recipe = new Recipe
            {
                Name = "Pasta",
                Description = "A delicious chocolate cake",
                PreparationTime = 10,
                CookingTime = 30,
                Portions = 8,
                Difficulty = 5,
                RecipeTypeId = recipeType.Id,
                RecipeNationalityId = recipeNationality.Id,
                CategoryId = category.Id,
            };

            await this.dbContext.Recipes.AddAsync(recipe);
            await this.dbContext.SaveChangesAsync();

            // Act
            await this.recipesService.DeleteAsync(recipe.Id);
            var deletedRecipe = await this.dbContext.Recipes.FindAsync(recipe.Id);

            // Assert
            Assert.That(deletedRecipe, Is.Null);
        }

        [Test]
        public async Task EditAsync_Should_Update_Recipe()
        {
            // Arrange
            var recipeType = await this.dbContext.RecipeTypes.FirstOrDefaultAsync();
            var recipeNationality = await this.dbContext.RecipeNationalities.FirstOrDefaultAsync();
            var category = await this.dbContext.Categories.FirstOrDefaultAsync();

            var recipe = new Recipe
            {
                Name = "Pasta",
                Description = "A delicious chocolate cake",
                PreparationTime = 10,
                CookingTime = 30,
                Portions = 8,
                Difficulty = 5,
                RecipeTypeId = recipeType.Id,
                RecipeNationalityId = recipeNationality.Id,
                CategoryId = category.Id,
            };

            await this.dbContext.Recipes.AddAsync(recipe);
            await this.dbContext.SaveChangesAsync();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Flour - 200g");
            sb.AppendLine("Sugar - 100g");
            sb.AppendLine("Cocoa - 50g");

            var editRecipeInput = new EditRecipeInputModel
            {
                Id = recipe.Id,
                Name = "Updated Pizza",
                Description = "Updated delicious pizza",
                PreparationTime = 15,
                CookingTime = 25,
                Portions = 4,
                Difficulty = 3,
                RecipeTypeId = 1,
                RecipeNationalityId = 1,
                CategoryId = 1,
                Ingredients = sb.ToString()
            };

            // Act
            await this.recipesService.EditAsync(editRecipeInput);
            var updatedRecipe = await this.dbContext.Recipes.FindAsync(recipe.Id);

            // Assert
            Assert.That(updatedRecipe, Is.Not.Null);
            Assert.That(updatedRecipe.Name, Is.EqualTo(editRecipeInput.Name));
            Assert.That(updatedRecipe.Description, Is.EqualTo(editRecipeInput.Description));
        }

        [Test]
        public async Task GetAllAsync_Should_Return_All_Recipes()
        {
            // Arrange
            var recipeType = await this.dbContext.RecipeTypes.FirstOrDefaultAsync();
            var recipeNationality = await this.dbContext.RecipeNationalities.FirstOrDefaultAsync();
            var category = await this.dbContext.Categories.FirstOrDefaultAsync();

            await this.dbContext.Recipes.AddRangeAsync(
                new Recipe
                {
                    Name = "Pasta",
                    Description = "A delicious chocolate cake",
                    PreparationTime = 10,
                    CookingTime = 30,
                    Portions = 8,
                    Difficulty = 5,
                    RecipeTypeId = recipeType.Id,
                    RecipeNationalityId = recipeNationality.Id,
                    CategoryId = category.Id,
                },
                new Recipe
                {
                    Name = "Soup",
                    Description = "A delicious chicken soup",
                    PreparationTime = 10,
                    CookingTime = 30,
                    Portions = 8,
                    Difficulty = 5,
                    RecipeTypeId = recipeType.Id,
                    RecipeNationalityId = recipeNationality.Id,
                    CategoryId = category.Id,
                }
            );

            await this.dbContext.SaveChangesAsync();

            // Act
            var expected = 2;
            var actual = await this.recipesService.GetAllAsync();

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(expected));
        }

        [Test]
        public async Task GetByIdAsync_Should_Return_Recipe_By_Id()
        {
            // Arrange
            var recipeType = await this.dbContext.RecipeTypes.FirstOrDefaultAsync();
            var recipeNationality = await this.dbContext.RecipeNationalities.FirstOrDefaultAsync();
            var category = await this.dbContext.Categories.FirstOrDefaultAsync();

            var recipe = new Recipe
            {
                Name = "Soup",
                Description = "A delicious chicken soup",
                PreparationTime = 10,
                CookingTime = 30,
                Portions = 8,
                Difficulty = 5,
                RecipeTypeId = recipeType.Id,
                RecipeNationalityId = recipeNationality.Id,
                CategoryId = category.Id,
            };

            await this.dbContext.Recipes.AddAsync(recipe);
            await this.dbContext.SaveChangesAsync();

            // Act
            var actual = await this.recipesService.GetByIdAsync(recipe.Id);

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Name, Is.EqualTo(recipe.Name));
        }
    }
}
