using Recipes.InputModels.Ingredients;
using Recipes.ViewModels.Ingredients;

namespace Recipes.Interfaces
{
    public interface IIngredientsService
    {
        Task CreateAsync(CreateIngredientInputModel createIngredientInputModel);

        Task<IEnumerable<IngredientViewModel>> GetAllAsync();

        Task DeleteAsync(int id);
    }
}
