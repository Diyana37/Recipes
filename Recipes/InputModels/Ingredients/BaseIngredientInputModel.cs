using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Recipes.InputModels.Ingredients
{
    public class BaseIngredientInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public int IngredientTypeId { get; set; }

        public IEnumerable<SelectListItem> IngredientTypeItems { get; set; }
    }
}
