
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.InputModels.Categories;
using Recipes.Interfaces;
using Recipes.Services;

namespace Recipes.Tests
{
    public class CategoriesServiceTests
    {
        private ApplicationDbContext dbContext;
        private ICategoriesService categoriesService;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "RecipesDB")
            .Options;

            this.dbContext = new ApplicationDbContext(options);
            this.categoriesService = new CategoriesService(this.dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted(); // Reset database after each test
            this.dbContext.Dispose();
        }

        [Test]
        public async Task CreateAsync_Should_Add_Category_To_Database()
        {
            // Arrange
            var categoryInput = new CreateCategoryInputModel { Name = "Dessert" };
            string expected = categoryInput.Name;

            // Act
            await this.categoriesService.CreateAsync(categoryInput);
            var actual = await this.dbContext.Categories.FirstOrDefaultAsync();

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Name, Is.EqualTo(expected));
        }

        [Test]
        public async Task DeleteAsync_Should_Remove_Category_From_Database()
        {
            // Arrange
            var category = new Category { Name = "Appetizer" };

            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();

            // Act
            await this.categoriesService.DeleteAsync(category.Id);
            var deletedCategory = await this.dbContext.Categories.FindAsync(category.Id);

            // Assert
            Assert.That(deletedCategory, Is.Null);
        }

        [Test]
        public async Task EditAsync_Should_Update_Category_Name()
        {
            // Arrange
            var category = new Category { Name = "Old Name" };

            await this.dbContext.Categories.AddAsync(category);
            await this.dbContext.SaveChangesAsync();
            var editInput = new EditCategoryInputModel { Id = category.Id, Name = "New Name" };

            // Act
            await this.categoriesService.EditAsync(editInput);
            var expected = editInput.Name;
            var actual = await this.dbContext.Categories.FindAsync(category.Id);

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Name, Is.EqualTo(expected));
        }

        [Test]
        public async Task GetAllAsync_Should_Return_All_Categories()
        {
            // Arrange
            await this.dbContext.Categories.AddRangeAsync(
                new Category { Name = "Category 1" },
                new Category { Name = "Category 2" }
            );

            await this.dbContext.SaveChangesAsync();

            // Act
            var expected = 2;
            var actual = await this.categoriesService.GetAllAsync();

            // Assert
            Assert.That(actual.Count(), Is.EqualTo(expected));
        }
    }
}
