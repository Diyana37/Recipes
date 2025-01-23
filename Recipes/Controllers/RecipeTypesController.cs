using Microsoft.AspNetCore.Mvc;
using Recipes.InputModels.IngredientTypes;
using Recipes.InputModels.RecipeTypes;
using Recipes.Interfaces;
using Recipes.ViewModels.RecipeTypes;

namespace Recipes.Controllers
{
    public class RecipeTypesController : Controller
    {
        private readonly IRecipeTypesService recipeTypesService;
        public RecipeTypesController(IRecipeTypesService recipeTypesService)
        {
            this.recipeTypesService = recipeTypesService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<RecipeTypeViewModel> recipeTypeViewModels = await this.recipeTypesService
                .GetAllAsync();

            return this.View(recipeTypeViewModels);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeTypeInputModel createRecipeTypeInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createRecipeTypeInputModel);
            }

            await this.recipeTypesService.CreateAsync(createRecipeTypeInputModel);

            return this.RedirectToAction("All", "RecipeTypes");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.recipeTypesService.DeleteAsync(id);
            this.TempData["Message"] = "Recipe Type is deleted successfully!";

            return this.RedirectToAction("All", "RecipeTypes");
        }
    }
}
