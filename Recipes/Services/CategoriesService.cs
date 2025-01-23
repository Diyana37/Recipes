using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.InputModels.Categories;
using Recipes.Interfaces;
using Recipes.ViewModels.Categories;

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

        public async Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync()
        {
            IEnumerable<SelectListItem> selectListItems = await this.dbContext.Categories
                            .Select(i => new SelectListItem
                            {
                                Value = i.Id.ToString(),
                                Text = i.Name,
                            })
                            .ToListAsync();

            return selectListItems;
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
