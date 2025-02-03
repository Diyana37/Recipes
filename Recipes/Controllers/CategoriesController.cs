using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Recipes.InputModels.Categories;
using Recipes.Interfaces;
using Recipes.ViewModels.Categories;

namespace Recipes.Controllers
{
    [Authorize(Roles = Constants.ADMINISTRATOR_ROLE)]
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

        public async Task<IActionResult> Edit(int id)
        {
            EditCategoryInputModel editCategoryInputModel = await this.categoriesService
                .GetByIdAsync(id);

            return this.View(editCategoryInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCategoryInputModel editCategoryInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editCategoryInputModel);
            }

            await this.categoriesService.EditAsync(editCategoryInputModel);

            return this.RedirectToAction("All", "Categories");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.categoriesService.DeleteAsync(id);

            return this.RedirectToAction("All", "Categories");
        }
    }
}
