using System.ComponentModel.DataAnnotations;

namespace Recipes.InputModels.IngredientTypes
{
    public class BaseIngredientTypeInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
