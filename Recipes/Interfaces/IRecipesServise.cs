using Microsoft.AspNetCore.Mvc.Rendering;
using Recipes.InputModels.Recipes;
using Recipes.ViewModels.Recipes;

namespace Recipes.Interfaces
{
    public interface IRecipesServise
    {
        Task CreateAsync(CreateRecipeInputModel createRecipeInputModel);

        Task<IEnumerable<RecipeViewModel>> GetAllAsync();
    }
}
