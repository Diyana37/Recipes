using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.InputModels.Recipes;
using Recipes.Interfaces;
using Recipes.ViewModels.Recipes;

namespace Recipes.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly ApplicationDbContext dbContext;

        public RecipesService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(CreateRecipeInputModel createRecipeInputModel)
        {
            Recipe recipe = new Recipe
            {
                Name = createRecipeInputModel.Name,
                Description = createRecipeInputModel.Description,
                PreparationTime = createRecipeInputModel.PreparationTime,
                CookingTime = createRecipeInputModel.CookingTime,
                Portions = createRecipeInputModel.Portions,
                Difficulty = createRecipeInputModel.Difficulty,
                RecipeTypeId = createRecipeInputModel.RecipeTypeId,
                RecipeNationalityId = createRecipeInputModel.RecipeNationalityId,
                CategoryId = createRecipeInputModel.CategoryId,
            };

            await this.dbContext.AddAsync(recipe);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Recipe recipe = await this.dbContext.Recipes
                            .FirstOrDefaultAsync(l => l.Id == id);

            this.dbContext.Recipes.Remove(recipe);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(EditRecipeInputModel editRecipeInputModel)
        {
            Recipe recipe = await this.dbContext.Recipes
                            .FirstOrDefaultAsync(r => r.Id == editRecipeInputModel.Id);

            recipe.Name = editRecipeInputModel.Name;
            recipe.Description = editRecipeInputModel.Description;
            recipe.PreparationTime = editRecipeInputModel.PreparationTime;
            recipe.CookingTime = editRecipeInputModel.CookingTime;
            recipe.Portions = editRecipeInputModel.Portions;
            recipe.Difficulty = editRecipeInputModel.Difficulty;
            recipe.RecipeTypeId = editRecipeInputModel.RecipeTypeId;
            recipe.RecipeNationalityId = editRecipeInputModel.RecipeNationalityId;
            recipe.CategoryId = editRecipeInputModel.CategoryId;

            this.dbContext.Recipes.Update(recipe);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<RecipeViewModel>> GetAllAsync()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.dbContext.Recipes
                .Include(r => r.RecipeType)
                .Include(r => r.RecipeNationality)
                .Include(r => r.Category)
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description= r.Description,
                    PreparationTime = r.PreparationTime,
                    CookingTime = r.CookingTime,
                    Portions = r.Portions,
                    Difficulty = r.Difficulty,
                    RecipeTypeId = r.RecipeTypeId,
                    RecipeTypeName = r.RecipeType.Name,
                    RecipeNationalityId = r.RecipeNationalityId,
                    RecipeNationalityName = r.RecipeNationality.Name,
                    CategoryId = r.CategoryId,
                    CategoryName = r.Category.Name
                })
                .ToListAsync();

            return recipeViewModels;
        }

        public async Task<EditRecipeInputModel> GetByIdAsync(int id)
        {
            EditRecipeInputModel editRecipeInputModel = await this.dbContext.Recipes
                .Where(r => r.Id == id)
                .Select(r => new EditRecipeInputModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    PreparationTime = r.PreparationTime,
                    CookingTime = r.CookingTime,
                    Portions = r.Portions,
                    Difficulty = r.Difficulty,
                    RecipeTypeId = r.RecipeTypeId,
                    RecipeNationalityId = r.RecipeNationalityId,
                    CategoryId = r.CategoryId
                })
                .FirstOrDefaultAsync();

            return editRecipeInputModel;
        }
    }
}
