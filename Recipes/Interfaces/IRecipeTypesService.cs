using Microsoft.AspNetCore.Mvc.Rendering;
using Recipes.InputModels.RecipeTypes;
using Recipes.ViewModels.RecipeTypes;

namespace Recipes.Interfaces
{
    public interface IRecipeTypesService
    {
        Task CreateAsync(CreateRecipeTypeInputModel createRecipeTypeInputModel);

        Task<IEnumerable<RecipeTypeViewModel>> GetAllAsync();

        Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync();

    }
}
