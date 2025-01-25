using System.ComponentModel.DataAnnotations;

namespace Recipes.InputModels.RecipeNationalities
{
    public class BaseRecipeNationalityInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
