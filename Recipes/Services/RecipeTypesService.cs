using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.InputModels.RecipeTypes;
using Recipes.Interfaces;
using Recipes.ViewModels.RecipeTypes;

namespace Recipes.Services
{
    public class RecipeTypesService : IRecipeTypesService
    {
        private readonly ApplicationDbContext dbContext;
        public RecipeTypesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CreateRecipeTypeInputModel createRecipeTypeInputModel)
        {
            RecipeType recipeType = new RecipeType
            {
                Name = createRecipeTypeInputModel.Name,
            };

            await this.dbContext.AddAsync(recipeType);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync()
        {
            IEnumerable<SelectListItem> selectListItems = await this.dbContext.RecipeTypes
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name,
                })
                .ToListAsync();

            return selectListItems;
        }

        public async Task<IEnumerable<RecipeTypeViewModel>> GetAllAsync()
        {
            IEnumerable<RecipeTypeViewModel> recipeTypeViewModels = await this.dbContext.RecipeTypes
                .Select(r => new RecipeTypeViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToListAsync();

            return recipeTypeViewModels;
        }
    }
}
