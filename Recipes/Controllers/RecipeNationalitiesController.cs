using Microsoft.AspNetCore.Mvc;
using Recipes.InputModels.IngredientTypes;
using Recipes.InputModels.RecipeNationalities;
using Recipes.Interfaces;
using Recipes.ViewModels.RecipeNationalities;

namespace Recipes.Controllers
{
    public class RecipeNationalitiesController : Controller
    {
        private readonly IRecipeNationalitiesService recipeNationalitiesService;
        public RecipeNationalitiesController(IRecipeNationalitiesService recipeNationalitiesService)
        {
            this.recipeNationalitiesService = recipeNationalitiesService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<RecipeNationalityViewModel> recipeNationalitiesViewModels = await this.recipeNationalitiesService
                .GetAllAsync();

            return View(recipeNationalitiesViewModels);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeNationalityInputModel createRecipeNationalityInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createRecipeNationalityInputModel);
            }

            await this.recipeNationalitiesService.CreateAsync(createRecipeNationalityInputModel);

            return this.RedirectToAction("All", "RecipeNationalities");
        }
    }
}
