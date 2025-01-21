using Microsoft.AspNetCore.Mvc;
using Recipes.Data;
using Recipes.InputModels.IngredientTypes;
using Recipes.Interfaces;
using Recipes.Services;
using Recipes.ViewModels.IngredientTypes;

namespace Recipes.Controllers
{
    public class IngredientTypesController : Controller
    {
        private readonly IIngredientTypesService ingredientTypesService;

        public IngredientTypesController(IIngredientTypesService ingredientTypesService)
        {
            this.ingredientTypesService = ingredientTypesService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<IngredientTypeViewModel> ingredientTypeViewModels = await this.ingredientTypesService
                .GetAllAsync();

            return this.View(ingredientTypeViewModels);
        }
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIngredientTypeInputModel createIngredientTypeInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createIngredientTypeInputModel);
            }

            await this.ingredientTypesService.CreateAsync(createIngredientTypeInputModel);

            return this.RedirectToAction("All", "IngredientTypes");
        }
    }
}
