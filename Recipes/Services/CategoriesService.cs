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

        public async Task DeleteAsync(int id)
        {
            Category category = await this.dbContext.Categories
                            .FirstOrDefaultAsync(l => l.Id == id);

            this.dbContext.Categories.Remove(category);

            await this.dbContext.SaveChangesAsync();        
        }

        public async Task EditAsync(EditCategoryInputModel editCategoryInputModel)
        {
            Category category = await this.dbContext.Categories
                            .FirstOrDefaultAsync(c => c.Id == editCategoryInputModel.Id);

            category.Name = editCategoryInputModel.Name;
            
            this.dbContext.Categories.Update(category);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync()
        {
            IEnumerable<SelectListItem> selectListItems = await this.dbContext.Categories
                            .OrderBy(c => c.Name.ToLower())
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
                .OrderBy(c => c.Name.ToLower())
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();

            return categoryViewModels;
        }

        public async Task<EditCategoryInputModel> GetByIdAsync(int id)
        {
            EditCategoryInputModel editCategoryInputModel = await this.dbContext.Categories
                .Where(c => c.Id == id)
                .Select(c => new EditCategoryInputModel
                {
                    Id = c.Id,
                    Name = c.Name 
                })
                .FirstOrDefaultAsync();

            return editCategoryInputModel;
        }
    }
}
