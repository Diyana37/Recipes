﻿using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.InputModels.Ingredients;
using Recipes.InputModels.RecipeTypes;
using Recipes.Interfaces;
using Recipes.ViewModels.Ingredients;

namespace Recipes.Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly ApplicationDbContext dbContext;
        public IngredientsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task CreateAsync(CreateIngredientInputModel createIngredientInputModel)
        {
            bool isCreated = await this.dbContext.Ingredients
                .AnyAsync(i => i.Name.ToLower() == createIngredientInputModel.Name.ToLower());

            if (isCreated)
            {
                return;
            }

            Ingredient ingredient = new Ingredient
            {
                Name = createIngredientInputModel.Name,
            };

            await this.dbContext.AddAsync(ingredient);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Ingredient ingredient = await this.dbContext.Ingredients
                            .FirstOrDefaultAsync(l => l.Id == id);

            this.dbContext.Ingredients.Remove(ingredient);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(EditIngredientInputModel editIngredientInputModel)
        {
            Ingredient ingredient = await this.dbContext.Ingredients
                            .FirstOrDefaultAsync(i => i.Id == editIngredientInputModel.Id);

            bool isCreated = await this.dbContext.Ingredients
                .AnyAsync(i => i.Name.ToLower() == editIngredientInputModel.Name.ToLower() 
                && i.Id != ingredient.Id);

            if (isCreated)
            {
                return;
            }

            ingredient.Name = editIngredientInputModel.Name;

            this.dbContext.Ingredients.Update(ingredient);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<IngredientViewModel>> GetAllAsync()
        {
            IEnumerable<IngredientViewModel> ingredientViewModels = await this.dbContext.Ingredients
                .OrderBy(i => i.Name.ToLower())
                .Select(i => new IngredientViewModel
                {
                    Id = i.Id,
                    Name = i.Name
                })
                .ToListAsync();

            return ingredientViewModels;
        }

        public async Task<EditIngredientInputModel> GetByIdAsync(int id)
        {
            EditIngredientInputModel editIngredientInputModel = await this.dbContext.Ingredients
                .Where(i => i.Id == id)
                .Select(i => new EditIngredientInputModel
                {
                    Id = i.Id,
                    Name = i.Name,
                })
                .FirstOrDefaultAsync();

            return editIngredientInputModel;
        }
    }
}
