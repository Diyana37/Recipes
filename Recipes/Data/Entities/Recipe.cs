using Recipes.Data.Entities.Identity;
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
        public string Description { get; set; }
        
        [Required]
        public int PreparationTime { get; set; }

        [Required]
        public int CookingTime { get; set; }

        [Required]
        public int Portions { get; set; }

        [Required]
        public int Difficulty { get; set; }

        [Url]
        public string Photo { get; set; }

        [Required]
        public int RecipeTypeId { get; set; }

        public RecipeType RecipeType { get; set; }

        [Required]
        public int RecipeNationalityId { get; set; }

        public RecipeNationality RecipeNationality { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string CreatorId { get; set; }

        public ApplicationUser Creator { get; set; }

        public ICollection<RecipeIngredient> Ingredients { get; set; }
    }
}
