using Recipes.ViewModels.Categories;
using Recipes.ViewModels.Ingredients;
using Recipes.ViewModels.RecipeNationalities;
using Recipes.ViewModels.Recipes;
using Recipes.ViewModels.RecipeTypes;

namespace Recipes.InputModels.Recipes
{
    public class FilterRecipeInputModel
    {
        public IEnumerable<int> CategoryIds { get; set; } = new List<int>();

        public IEnumerable<int> RecipeNationalityIds { get; set; } = new List<int>();

        public IEnumerable<int> RecipeTypeIds { get; set; } = new List<int>();

        public IEnumerable<int> IngredientIds { get; set; } = new List<int>();

        public IEnumerable<int> Portions { get; set; } = new List<int>();

        public string SearchText { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; }

        public IEnumerable<RecipeNationalityViewModel> RecipeNationalities { get; set; }

        public IEnumerable<RecipeTypeViewModel> RecipeTypes { get; set; }

        public IEnumerable<IngredientViewModel> Ingredients { get; set; }

        public IEnumerable<RecipeViewModel> Recipes { get; set; }
    }
}
