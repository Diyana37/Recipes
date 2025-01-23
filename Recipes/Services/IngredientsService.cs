using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.InputModels.Ingredients;
using Recipes.Interfaces;
using Recipes.ViewModels.Ingredients;

namespace Recipes.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly ApplicationDbContext dbContext;
        public IngredientsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(CreateIngredientInputModel createIngredientInputModel)
        {
            Ingredient ingredient = new Ingredient
            {
                Name = createIngredientInputModel.Name,
                IngredientTypeId = createIngredientInputModel.IngredientTypeId
            };

            await this.dbContext.AddAsync(ingredient);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Ingredient ingredient = await this.dbContext.Ingredients
                            .FirstOrDefaultAsync(l => l.Id == id);

            this.dbContext.Ingredients.Remove(ingredient);

            await this.dbContext.SaveChangesAsync();
        }

        public Task EditAsync(EditIngredientInputModel editIngredientInputModel)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IngredientViewModel>> GetAllAsync()
        {
            IEnumerable<IngredientViewModel> ingredientViewModels = await this.dbContext.Ingredients
                .Include(i => i.IngredientType)
                .Select(i => new IngredientViewModel
                {
                    Id = i.Id,
                    Name = i.Name,
                    IngredientTypeId = i.IngredientTypeId,
                    IngredientTypeName = i.IngredientType.Name
                })
                .ToListAsync();

            return ingredientViewModels;
        }

        public Task<EditIngredientInputModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
