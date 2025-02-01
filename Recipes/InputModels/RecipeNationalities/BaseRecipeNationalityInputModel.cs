using System.ComponentModel.DataAnnotations;

namespace Recipes.InputModels.RecipeNationalities
{
    public class BaseRecipeNationalityInputModel
    {
        [Required(ErrorMessage = "Името е задължително!")]
        [MinLength(3, ErrorMessage = "Името трябва да е минимум 3 символа!")]
        [MaxLength(50, ErrorMessage = "Името трябва да е максимум 50 символа!")]
        public string Name { get; set; }
    }
}
