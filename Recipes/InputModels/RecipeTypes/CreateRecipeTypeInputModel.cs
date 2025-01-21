using System.ComponentModel.DataAnnotations;

namespace Recipes.InputModels.RecipeTypes
{
    public class CreateRecipeTypeInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
