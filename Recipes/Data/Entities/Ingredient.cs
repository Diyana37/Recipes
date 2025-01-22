using System.ComponentModel.DataAnnotations;

namespace Recipes.Data.Entities
{
    public class Ingredient
    {
        public Ingredient() 
        {
            this.Recipes = new HashSet<RecipeIngredient>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int IngredientTypeId { get; set; }

        public IngredientType IngredientType { get; set; }

        public ICollection<RecipeIngredient> Recipes { get; set; }
    }
}
