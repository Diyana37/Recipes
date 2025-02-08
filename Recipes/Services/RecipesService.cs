using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.Data.Entities.Identity;
using Recipes.InputModels.Recipes;
using Recipes.Interfaces;
using Recipes.ViewModels.RecipeIngredients;
using Recipes.ViewModels.Recipes;
using System.Text;

namespace Recipes.Services
{
    public class RecipesService : IRecipesService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ICloudinaryService cloudinaryService;

        public RecipesService(ApplicationDbContext dbContext, ICloudinaryService cloudinaryService)
        {
            this.dbContext = dbContext;
            this.cloudinaryService = cloudinaryService;
        }
        public async Task CreateAsync(CreateRecipeInputModel createRecipeInputModel, string userId)
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
                CreatorId = userId
            };

            if (createRecipeInputModel.FormFile != null && createRecipeInputModel.FormFile.Length > 0)
            {
                string photo = await this.cloudinaryService
                    .UploadAsync(createRecipeInputModel.FormFile);

                recipe.Photo = photo;
            }

            await this.dbContext.AddAsync(recipe);
            await this.dbContext.SaveChangesAsync();

            await this.SplitIngredientsAsync(createRecipeInputModel.Ingredients, recipe.Id);
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

            if (editRecipeInputModel.FormFile != null && editRecipeInputModel.FormFile.Length > 0)
            {
                string photo = await this.cloudinaryService
                    .UploadAsync(editRecipeInputModel.FormFile);

                recipe.Photo = photo;
            }

            this.dbContext.Recipes.Update(recipe);
            await this.dbContext.SaveChangesAsync();

            await this.RemoveRecipeIngredientsAsync(recipe.Id);
            await this.SplitIngredientsAsync(editRecipeInputModel.Ingredients, recipe.Id);
        }

        public async Task<IEnumerable<RecipeViewModel>> GetAllAsync()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.dbContext.Recipes
                .Include(r => r.RecipeType)
                .Include(r => r.RecipeNationality)
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    PreparationTime = r.PreparationTime,
                    CookingTime = r.CookingTime,
                    Portions = r.Portions,
                    Difficulty = r.Difficulty,
                    RecipeTypeId = r.RecipeTypeId,
                    RecipeTypeName = r.RecipeType.Name,
                    RecipeNationalityId = r.RecipeNationalityId,
                    RecipeNationalityName = r.RecipeNationality.Name,
                    CategoryId = r.CategoryId,
                    CategoryName = r.Category.Name,
                    Photo = r.Photo,
                    Ingredients = r.Ingredients
                    .Select(i => new RecipeIngredientViewModel
                    {
                        IngredientName = i.Ingredient.Name,
                        Quantity = i.Quantity
                    })
                    .ToList()
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
                    CategoryId = r.CategoryId,
                    Photo = r.Photo
                })
                .FirstOrDefaultAsync();

            editRecipeInputModel.Ingredients = await this.BuildIngredientsTextAreaAsync(editRecipeInputModel.Id);

            return editRecipeInputModel;
        }

        public async Task<IEnumerable<RecipeViewModel>> GetFilteredWithPaginationAsync(FilterRecipeInputModel filterRecipeInputModel)
        {
            IQueryable<Recipe> recipes = this.dbContext.Recipes
                .Include(r => r.RecipeType)
                .Include(r => r.RecipeNationality)
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterRecipeInputModel.SearchText))
            {
                recipes = recipes
                    .Where(r => r.Name.ToLower().Contains(filterRecipeInputModel.SearchText.ToLower())
                    || r.Ingredients.Any(i => i.Ingredient.Name.ToLower().Contains(filterRecipeInputModel.SearchText.ToLower())));
            }

            if (filterRecipeInputModel.CategoryIds.Count() > 0)
            {
                recipes = recipes
                    .Where(r => filterRecipeInputModel.CategoryIds.Any(c => c == r.CategoryId));
            }

            IEnumerable<RecipeViewModel> recipeViewModels = await recipes
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    PreparationTime = r.PreparationTime,
                    CookingTime = r.CookingTime,
                    Portions = r.Portions,
                    Difficulty = r.Difficulty,
                    RecipeTypeId = r.RecipeTypeId,
                    RecipeTypeName = r.RecipeType.Name,
                    RecipeNationalityId = r.RecipeNationalityId,
                    RecipeNationalityName = r.RecipeNationality.Name,
                    CategoryId = r.CategoryId,
                    CategoryName = r.Category.Name,
                    Photo = r.Photo,
                    Ingredients = r.Ingredients
                    .Select(i => new RecipeIngredientViewModel
                    {
                        IngredientName = i.Ingredient.Name,
                        Quantity = i.Quantity
                    })
                    .ToList()
                })
                .ToListAsync();

            return recipeViewModels;
        }

        public async Task<IEnumerable<RecipeViewModel>> GetNewAsync()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.dbContext.Recipes
                .OrderByDescending(r => r.Id)
                .Take(3)
                .Include(r => r.RecipeType)
                .Include(r => r.RecipeNationality)
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    PreparationTime = r.PreparationTime,
                    CookingTime = r.CookingTime,
                    Portions = r.Portions,
                    Difficulty = r.Difficulty,
                    RecipeTypeId = r.RecipeTypeId,
                    RecipeTypeName = r.RecipeType.Name,
                    RecipeNationalityId = r.RecipeNationalityId,
                    RecipeNationalityName = r.RecipeNationality.Name,
                    CategoryId = r.CategoryId,
                    CategoryName = r.Category.Name,
                    Photo = r.Photo,
                    Ingredients = r.Ingredients
                    .Select(i => new RecipeIngredientViewModel
                    {
                        IngredientName = i.Ingredient.Name,
                        Quantity = i.Quantity
                    })
                    .ToList()
                })
                .ToListAsync();

            return recipeViewModels;
        }

        public async Task<IEnumerable<RecipeViewModel>> GetRandomAsync()
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.dbContext.Recipes
                .OrderByDescending(r => Guid.NewGuid())
                .Take(3)
                .Include(r => r.RecipeType)
                .Include(r => r.RecipeNationality)
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    PreparationTime = r.PreparationTime,
                    CookingTime = r.CookingTime,
                    Portions = r.Portions,
                    Difficulty = r.Difficulty,
                    RecipeTypeId = r.RecipeTypeId,
                    RecipeTypeName = r.RecipeType.Name,
                    RecipeNationalityId = r.RecipeNationalityId,
                    RecipeNationalityName = r.RecipeNationality.Name,
                    CategoryId = r.CategoryId,
                    CategoryName = r.Category.Name,
                    Photo = r.Photo,
                    Ingredients = r.Ingredients
                    .Select(i => new RecipeIngredientViewModel
                    {
                        IngredientName = i.Ingredient.Name,
                        Quantity = i.Quantity
                    })
                    .ToList()
                })
                .ToListAsync();

            return recipeViewModels;
        }

        public async Task<IEnumerable<RecipeViewModel>> GetByCreatorIdAsync(string userId)
        {
            IEnumerable<RecipeViewModel> recipeViewModels = await this.dbContext.Recipes
                .Include(r => r.RecipeType)
                .Include(r => r.RecipeNationality)
                .Include(r => r.Category)
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.Ingredient)
                .Where(i => i.CreatorId == userId)
                .Select(r => new RecipeViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    PreparationTime = r.PreparationTime,
                    CookingTime = r.CookingTime,
                    Portions = r.Portions,
                    Difficulty = r.Difficulty,
                    RecipeTypeId = r.RecipeTypeId,
                    RecipeTypeName = r.RecipeType.Name,
                    RecipeNationalityId = r.RecipeNationalityId,
                    RecipeNationalityName = r.RecipeNationality.Name,
                    CategoryId = r.CategoryId,
                    CategoryName = r.Category.Name,
                    Photo = r.Photo,
                    CreatorId = userId,
                    Ingredients = r.Ingredients
                    .Select(i => new RecipeIngredientViewModel
                    {
                        IngredientName = i.Ingredient.Name,
                        Quantity = i.Quantity
                    })
                    .ToList()
                })
                .ToListAsync();

            return recipeViewModels;
        }

        private async Task SplitIngredientsAsync(string ingredients, int recipeId)
        {
            string[] ingredientLines = ingredients.Split(Environment.NewLine);

            if (ingredientLines.Length > 0)
            {
                foreach (var item in ingredientLines)
                {
                    string[] ingredientRow = item.Split('-');

                    if (ingredientRow.Length > 1)
                    {
                        string ingredientName = ingredientRow[0].Trim();
                        string quantity = ingredientRow[1].Trim();

                        Ingredient ingredient = await this.dbContext.Ingredients
                            .FirstOrDefaultAsync(i => i.Name.ToLower() == ingredientName.ToLower());

                        if (ingredient != null)
                        {
                            RecipeIngredient recipeIngredient = new RecipeIngredient();
                            recipeIngredient.IngredientId = ingredient.Id;
                            recipeIngredient.RecipeId = recipeId;
                            recipeIngredient.Quantity = quantity;

                            await this.dbContext.RecipeIngredients.AddAsync(recipeIngredient);
                            await this.dbContext.SaveChangesAsync();
                        }
                        else
                        {
                            Ingredient newIngredient = new Ingredient();
                            newIngredient.Name = ingredientName;

                            await this.dbContext.Ingredients.AddAsync(newIngredient);
                            await this.dbContext.SaveChangesAsync();

                            RecipeIngredient recipeIngredient = new RecipeIngredient();
                            recipeIngredient.IngredientId = newIngredient.Id;
                            recipeIngredient.RecipeId = recipeId;
                            recipeIngredient.Quantity = quantity;

                            await this.dbContext.RecipeIngredients.AddAsync(recipeIngredient);
                            await this.dbContext.SaveChangesAsync();
                        }
                    }
                }
            }
        }

        private async Task<string> BuildIngredientsTextAreaAsync(int recipeId)
        {
            Recipe recipe = await this.dbContext.Recipes
                            .Include(r => r.Ingredients)
                            .ThenInclude(i => i.Ingredient)
                            .FirstOrDefaultAsync(r => r.Id == recipeId);

            StringBuilder sb = new StringBuilder();

            foreach (var ingredient in recipe.Ingredients)
            {
                sb.AppendLine($"{ingredient.Ingredient.Name} - {ingredient.Quantity}");
            }

            return sb.ToString();
        }

        private async Task RemoveRecipeIngredientsAsync(int recipeId)
        {
            Recipe recipe = await this.dbContext.Recipes
                            .Include(r => r.Ingredients)
                            .ThenInclude(i => i.Ingredient)
                            .FirstOrDefaultAsync(r => r.Id == recipeId);

            IEnumerable<RecipeIngredient> recipeIngredients = recipe.Ingredients;

            this.dbContext.RecipeIngredients.RemoveRange(recipeIngredients);

            await this.dbContext.SaveChangesAsync();
        }
    }
}
