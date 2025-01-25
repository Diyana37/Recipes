using Recipes.InputModels.Recipes;
using Recipes.ViewModels.Recipes;

namespace Recipes.Interfaces
{
    public interface IRecipesService
    {
        Task CreateAsync(CreateRecipeInputModel createRecipeInputModel);

        Task<IEnumerable<RecipeViewModel>> GetAllAsync();

        Task DeleteAsync(int id);

        Task EditAsync(EditRecipeInputModel editRecipeInputModel);

        Task<EditRecipeInputModel> GetByIdAsync(int id);
    }
}
