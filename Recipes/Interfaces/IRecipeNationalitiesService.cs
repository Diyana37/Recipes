using Recipes.InputModels.RecipeNationalities;
using Recipes.ViewModels.RecipeNationalities;

namespace Recipes.Interfaces
{
    public interface IRecipeNationalitiesService
    {
        Task CreateAsync(CreateRecipeNationalityInputModel createRecipeNationalityInputModel);

        Task<IEnumerable<RecipeNationalityViewModel>> GetAllAsync();
    }
}
