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
