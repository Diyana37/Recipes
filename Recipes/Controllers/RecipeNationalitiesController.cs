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

        public async Task<IActionResult> Edit(int id)
        {
            EditRecipeNationalityInputModel editRecipeNationalityInputModel = await this.recipeNationalitiesService
                .GetByIdAsync(id);

            return this.View(editRecipeNationalityInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRecipeNationalityInputModel editRecipeNationalityInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editRecipeNationalityInputModel);
            }

            await this.recipeNationalitiesService.EditAsync(editRecipeNationalityInputModel);

            return this.RedirectToAction("All", "RecipeNationalities");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.recipeNationalitiesService.DeleteAsync(id);

            return this.RedirectToAction("All", "RecipeNationalities");
        }
    }
}
