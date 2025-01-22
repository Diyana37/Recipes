using System.ComponentModel.DataAnnotations;

namespace Recipes.Data.Entities
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Quantity { get; set; }

        [Required]
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        [Required]
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
