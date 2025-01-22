using Microsoft.AspNetCore.Mvc;
using Recipes.InputModels.Ingredients;
using Recipes.Interfaces;
using Recipes.ViewModels.Ingredients;

namespace Recipes.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IIngredientsService ingredientsService;
        private readonly IIngredientTypesService ingredientTypesService;

        public IngredientsController(IIngredientsService ingredientsService, IIngredientTypesService ingredientTypesService)
        {
            this.ingredientsService = ingredientsService;
            this.ingredientTypesService = ingredientTypesService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<IngredientViewModel> ingredientViewModels = await this.ingredientsService
                .GetAllAsync();

            return this.View(ingredientViewModels);
        }

        public async Task<IActionResult> Create()
        {
            CreateIngredientInputModel createIngredientInputModel = new CreateIngredientInputModel
            {
                IngredientTypeItems = await this.ingredientTypesService.GetAllAsItemsAsync()
            };

            return this.View(createIngredientInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIngredientInputModel createIngredientInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                createIngredientInputModel.IngredientTypeItems = await this.ingredientTypesService
                    .GetAllAsItemsAsync();

                return this.View(createIngredientInputModel);
            }

            await this.ingredientsService.CreateAsync(createIngredientInputModel);

            return this.RedirectToAction("All", "Ingredients");
        }
    }
}
