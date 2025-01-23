using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.InputModels.RecipeNationalities;
using Recipes.Interfaces;
using Recipes.ViewModels.RecipeNationalities;

namespace Recipes.Services
{
    public class RecipeNationalitiesService : IRecipeNationalitiesService
    {
        private readonly ApplicationDbContext dbContext;
        public RecipeNationalitiesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CreateRecipeNationalityInputModel createRecipeNationalityInputModel)
        {
            RecipeNationality recipeNationality = new RecipeNationality
            {
                Name = createRecipeNationalityInputModel.Name,
            };

            await this.dbContext.AddAsync(recipeNationality);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsItemsAsync()
        {
            IEnumerable<SelectListItem> selectListItems = await this.dbContext.RecipeNationalities
                .Select(i => new SelectListItem
                {
                    Value = i.Id.ToString(),
                    Text = i.Name,
                })
                .ToListAsync();

            return selectListItems;
        }

        public async Task<IEnumerable<RecipeNationalityViewModel>> GetAllAsync()
        {
            IEnumerable<RecipeNationalityViewModel> recipeNationalitiesViewModels = await this.dbContext.RecipeNationalities
                .Select(r => new RecipeNationalityViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                }).ToListAsync();

            return recipeNationalitiesViewModels;
        }
    }
}
