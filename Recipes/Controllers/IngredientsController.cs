using Microsoft.AspNetCore.Mvc;
using Recipes.InputModels.Ingredients;
using Recipes.Interfaces;
using Recipes.ViewModels.Ingredients;

namespace Recipes.Controllers
{
    public class IngredientsController : Controller
    {
        private readonly IIngredientsService ingredientsService;

        public IngredientsController(IIngredientsService ingredientsService)
        {
            this.ingredientsService = ingredientsService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<IngredientViewModel> ingredientViewModels = await this.ingredientsService
                .GetAllAsync();

            return this.View(ingredientViewModels);
        }

        public async Task<IActionResult> Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIngredientInputModel createIngredientInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createIngredientInputModel);
            }

            await this.ingredientsService.CreateAsync(createIngredientInputModel);

            return this.RedirectToAction("All", "Ingredients");
        }

        public async Task<IActionResult> Edit(int id)
        {
            EditIngredientInputModel editIngredientInputModel = await this.ingredientsService
                .GetByIdAsync(id);

            return this.View(editIngredientInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditIngredientInputModel editIngredientInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editIngredientInputModel);
            }

            await this.ingredientsService.EditAsync(editIngredientInputModel);

            return this.RedirectToAction("All", "Ingredients");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.ingredientsService.DeleteAsync(id);

            return this.RedirectToAction("All", "Ingredients");
        }
    }
}
