using Humanizer.Localisation;
using System.ComponentModel.DataAnnotations;

namespace Recipes.Data.Entities
{
    public class Recipe
    {
        public Recipe() 
        { 
            this.Ingredients = new HashSet<RecipeIngredient>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [Required]
        [Range(3, 1000)]
        public int PreparationTime { get; set; }

        [Required]
        [Range(3, 1000)]
        public int CookingTime { get; set; }

        [Required]
        [Range(1, 100)]
        public int Portions { get; set; }

        [Required]
        [Range(1, 10)]
        public int Difficulty { get; set; }

        [Url]
        public string Photo { get; set; }

        public int RecipeTypeId { get; set; }

        public RecipeType RecipeType { get; set; }

        public int RecipeNationalityId { get; set; }
        public RecipeNationality RecipeNationality { get; set; }

        public int CategoryID { get; set; }
        public Category Category { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; }
    }
}
