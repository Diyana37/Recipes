using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.InputModels.IngredientTypes;
using Recipes.Interfaces;
using Recipes.ViewModels.IngredientTypes;

namespace Recipes.Services
{
    public class IngredientTypesService : IIngredientTypesService
    {
        private readonly ApplicationDbContext dbContext;
        public IngredientTypesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(CreateIngredientTypeInputModel createIngredientTypeInputModel)
        {
            IngredientType ingredientType = new IngredientType
            {
                Name = createIngredientTypeInputModel.Name,
            };

            await dbContext.AddAsync(ingredientType);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            IngredientType ingredientType = await this.dbContext.IngredientTypes
                                        .FirstOrDefaultAsync(l => l.Id == id);

            this.dbContext.IngredientTypes.Remove(ingredientType);

            await this.dbContext.SaveChangesAsync();
        }

        public Task EditAsync(EditIngredientTypeInputModel editIngredientTypeInputModel)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync()
        {
            IEnumerable<SelectListItem> selectListItems = await this.dbContext.IngredientTypes
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name,
                })
                .ToListAsync();

            return selectListItems;
        }

        public async Task<IEnumerable<IngredientTypeViewModel>> GetAllAsync()
        {
            IEnumerable<IngredientTypeViewModel> ingredientTypeViewModels = await this.dbContext.IngredientTypes
                .Select(i => new IngredientTypeViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                })
                .ToListAsync(); 

            return ingredientTypeViewModels;
        }

        public Task<EditIngredientTypeInputModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
