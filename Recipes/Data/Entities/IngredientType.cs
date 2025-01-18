using System.ComponentModel.DataAnnotations;

namespace Recipes.Data.Entities
{
    public class IngredientType
    {
        public IngredientType() 
        {
            this.Ingredients = new HashSet<Ingredient>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }

    }
}
