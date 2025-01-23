using Microsoft.AspNetCore.Mvc;
using Recipes.InputModels.Ingredients;
using Recipes.InputModels.Recipes;
using Recipes.Interfaces;
using Recipes.ViewModels.Recipes;

namespace Recipes.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesServise recipesServise;
        private readonly IRecipeTypesService recipeTypesService;
        private readonly IRecipeNationalitiesService recipeNationalitiesService;
        private readonly ICategoriesService categoriesService;

        public RecipesController(IRecipesServise recipesServise,
            IRecipeTypesService recipeTypesService,
            IRecipeNationalitiesService recipeNationalitiesService,
            ICategoriesService categoriesService)
        {
            this.recipesServise = recipesServise;
            this.recipeTypesService = recipeTypesService;
            this.recipeNationalitiesService = recipeNationalitiesService;
            this.categoriesService = categoriesService;
        }
        public async Task<IActionResult> All()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.recipesServise
                .GetAllAsync();

            return this.View(recipeViewModels);
        }

        
        public async Task<IActionResult> Create()
        {
            CreateRecipeInputModel createRecipeInputModel = new CreateRecipeInputModel
            {
                RecipeTypeItems = await this.recipeTypesService.GetAllAsItemsAsync(),
                RecipeNationalityItems = await this.recipeNationalitiesService.GetAllAsItemsAsync(),
                CategoryItems = await this.categoriesService.GetAllAsItemsAsync()
            };

            return this.View(createRecipeInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeInputModel createRecipeInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                createRecipeInputModel.RecipeTypeItems = await this.recipeTypesService
                    .GetAllAsItemsAsync();

                createRecipeInputModel.RecipeNationalityItems = await this.recipeNationalitiesService
                    .GetAllAsItemsAsync();

                createRecipeInputModel.CategoryItems = await this.categoriesService
                    .GetAllAsItemsAsync();

                return this.View(createRecipeInputModel);
            }

            await this.recipesServise.CreateAsync(createRecipeInputModel);

            return this.RedirectToAction("All", "Recipes");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.recipesServise.DeleteAsync(id);
            this.TempData["Message"] = "Recipe is deleted successfully!";

            return this.RedirectToAction("All", "Recipes");
        }
    }
}
