using Microsoft.AspNetCore.Mvc;
using Recipes.InputModels.Ingredients;
using Recipes.InputModels.Recipes;
using Recipes.Interfaces;
using Recipes.ViewModels.Recipes;

namespace Recipes.Controllers
{
    public class RecipesController : Controller
    {
        private readonly IRecipesService recipesService;
        private readonly IRecipeTypesService recipeTypesService;
        private readonly IRecipeNationalitiesService recipeNationalitiesService;
        private readonly ICategoriesService categoriesService;

        public RecipesController(IRecipesService recipesService,
            IRecipeTypesService recipeTypesService,
            IRecipeNationalitiesService recipeNationalitiesService,
            ICategoriesService categoriesService)
        {
            this.recipesService = recipesService;
            this.recipeTypesService = recipeTypesService;
            this.recipeNationalitiesService = recipeNationalitiesService;
            this.categoriesService = categoriesService;
        }
        public async Task<IActionResult> All()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.recipesService
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

            await this.recipesService.CreateAsync(createRecipeInputModel);

            return this.RedirectToAction("All", "Recipes");
        }

        public async Task<IActionResult> Edit(int id)
        {
            EditRecipeInputModel editRecipeInputModel = await this.recipesService
                .GetByIdAsync(id);

            editRecipeInputModel.RecipeTypeItems = await this.recipeTypesService
                    .GetAllAsItemsAsync();

            editRecipeInputModel.RecipeNationalityItems = await this.recipeNationalitiesService
                .GetAllAsItemsAsync();

            editRecipeInputModel.CategoryItems = await this.categoriesService
                .GetAllAsItemsAsync();

            return this.View(editRecipeInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRecipeInputModel editRecipeInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                editRecipeInputModel.RecipeTypeItems = await this.recipeTypesService
                    .GetAllAsItemsAsync();

                editRecipeInputModel.RecipeNationalityItems = await this.recipeNationalitiesService
                .GetAllAsItemsAsync();

                editRecipeInputModel.CategoryItems = await this.categoriesService
                    .GetAllAsItemsAsync();

                return this.View(editRecipeInputModel);
            }

            await this.recipesService.EditAsync(editRecipeInputModel);

            return this.RedirectToAction("All", "Recipes");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.recipesService.DeleteAsync(id);

            return this.RedirectToAction("All", "Recipes");
        }
    }
}
