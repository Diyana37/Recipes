using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Recipes.InputModels.Categories;
using Recipes.Interfaces;
using Recipes.ViewModels.Categories;

namespace Recipes.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        public CategoriesController(ICategoriesService categoriesService) 
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<CategoryViewModel> categoryViewModels = await this.categoriesService
                .GetAllAsync();

            return this.View(categoryViewModels);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryInputModel createCategoryInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(createCategoryInputModel);
            }

            await this.categoriesService.CreateAsync(createCategoryInputModel);

            return this.RedirectToAction("All", "Categories");
        }
    }
}
