using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Recipes.Data.Entities.Identity;
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
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(IRecipesService recipesService,
            IRecipeTypesService recipeTypesService,
            IRecipeNationalitiesService recipeNationalitiesService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.recipeTypesService = recipeTypesService;
            this.recipeNationalitiesService = recipeNationalitiesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        [Authorize(Roles = Constants.ADMINISTRATOR_ROLE)]
        public async Task<IActionResult> List()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.recipesService
                .GetAllAsync();

            return this.View(recipeViewModels);
        }

        [Authorize]
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

        [Authorize]
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

            var user = await this.userManager.GetUserAsync(this.User);
            await this.recipesService.CreateAsync(createRecipeInputModel, user.Id);

            return this.RedirectToAction("List", "Recipes");
        }

        [Authorize]
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

        [Authorize]
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

            return this.RedirectToAction("List", "Recipes");
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await this.recipesService.DeleteAsync(id);

            return this.RedirectToAction("List", "Recipes");
        }
        public async Task<IActionResult> New()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.recipesService
                .GetNewAsync();

            return this.View(recipeViewModels);
        }

        public async Task<IActionResult> Random()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.recipesService
                .GetRandomAsync();

            return this.View(recipeViewModels);
        }

        public async Task<IActionResult> FilteredWithPagination()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.recipesService
                .GetFilteredWithPaginationAsync();

            return this.View(recipeViewModels);
        }
    }
}
