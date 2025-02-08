using Recipes.ViewModels.Categories;
using Recipes.ViewModels.Recipes;

namespace Recipes.InputModels.Recipes
{
    public class FilterRecipeInputModel
    {
        public IEnumerable<int> CategoryIds { get; set; } = new List<int>();

        public string SearchText { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<RecipeViewModel> Recipes { get; set; }
    }
}
