using Microsoft.AspNetCore.Mvc.Rendering;
using Recipes.InputModels.Categories;
using Recipes.ViewModels.Categories;

namespace Recipes.Interfaces
{
    public interface ICategoriesService
    {
        Task CreateAsync(CreateCategoryInputModel createCategoryInputModel);

        Task<IEnumerable<CategoryViewModel>> GetAllAsync();

        Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync();

        Task DeleteAsync(int id);

        Task EditAsync(EditCategoryInputModel editCategoryInputModel);

        Task<EditCategoryInputModel> GetByIdAsync(int id);
    }
}
