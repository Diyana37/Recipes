using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Recipes.InputModels.Recipes
{
    public class BaseRecipeInputModel
    {
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

        //[Url]
        //public string Photo { get; set; }

        [Required]
        public int RecipeTypeId { get; set; }


        [Required]
        public int RecipeNationalityId { get; set; }


        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> RecipeTypeItems { get; set; }

        public IEnumerable<SelectListItem> RecipeNationalityItems { get; set; }

        public IEnumerable<SelectListItem> CategoryItems { get; set; }
    }
}
