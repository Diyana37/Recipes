using Recipes.InputModels.Categories;
using Recipes.ViewModels.Categories;

namespace Recipes.Interfaces
{
    public interface ICategoriesService
    {
        Task CreateAsync(CreateCategoryInputModel createCategoryInputModel);

        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
    }
}
