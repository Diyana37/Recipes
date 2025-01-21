
using Recipes.InputModels.IngredientTypes;
using Recipes.ViewModels.IngredientTypes;

namespace Recipes.Interfaces
{
    public interface IIngredientTypesService
    {
        Task CreateAsync(CreateIngredientTypeInputModel createIngredientTypeInputModel);

        Task<IEnumerable<IngredientTypeViewModel>> GetAllAsync();
    }
}
