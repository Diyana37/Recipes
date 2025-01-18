using System.ComponentModel.DataAnnotations;

namespace Recipes.Data.Entities
{
    public class RecipeNationality
    {
        public RecipeNationality()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Recipe> Recipes { get; set; }

    }
}
