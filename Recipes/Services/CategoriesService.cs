using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.InputModels.Categories;
using Recipes.Interfaces;
using Recipes.ViewModels.Categories;
using System.Runtime.InteropServices;

namespace Recipes.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ApplicationDbContext dbContext;
        public CategoriesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CreateCategoryInputModel createCategoryInputModel)
        {
            Category category = new Category
            {
                Name = createCategoryInputModel.Name
            };

            await this.dbContext.AddAsync(category);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            IEnumerable<CategoryViewModel> categoryViewModels = await this.dbContext.Categories
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return categoryViewModels;
        }
    }
}
