using Microsoft.AspNetCore.Mvc.Rendering;
using Recipes.InputModels.RecipeNationalities;
using Recipes.ViewModels.RecipeNationalities;

namespace Recipes.Interfaces
{
    public interface IRecipeNationalitiesService
    {
        Task CreateAsync(CreateRecipeNationalityInputModel createRecipeNationalityInputModel);

        Task<IEnumerable<RecipeNationalityViewModel>> GetAllAsync();

        Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync();

        Task DeleteAsync(int id);
    }
}
