using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Recipes.InputModels.Recipes
{
    public class BaseRecipeInputModel
    {
        [Required(ErrorMessage = "Името е задължително!")]
        [MinLength(3, ErrorMessage = "Името трябва да е минимум 3 символа!")]
        [MaxLength(50, ErrorMessage = "Името трябва да е максимум 50 символа!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Описанието е задължително!")]
        [MinLength(3, ErrorMessage = "Описанието трябва да е минимум 3 символа!")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Продуктите са задължителни!")]
        public string Ingredients { get; set; }

        [Required(ErrorMessage = "Времето за приготвяне е задължително!")]
        [Range(3, 1000, ErrorMessage = "Времето за приготвяне трябва да е между 3 и 1000 символа!")]
        public int PreparationTime { get; set; }

        [Required(ErrorMessage = "Времето за готвене е задължително!")]
        [Range(3, 1000, ErrorMessage = "Времето за готвене трябва да е между 3 и 1000 символа!")]
        public int CookingTime { get; set; }

        [Required(ErrorMessage = "Порциите са задължителни!")]
        [Range(1, 100, ErrorMessage = "Порциите трябва да са между 1 и 100 символа!")]
        public int Portions { get; set; }

        [Required(ErrorMessage = "Трудността е задължителна!")]
        [Range(1, 10, ErrorMessage = "Трудността трябва да е между 1 и 10 символа!")]
        public int Difficulty { get; set; }

        public IFormFile FormFile { get; set; }

        [Required(ErrorMessage = "Типът е задължителен!")]
        public int RecipeTypeId { get; set; }


        [Required(ErrorMessage = "Националността е задължителна!")]
        public int RecipeNationalityId { get; set; }


        [Required(ErrorMessage = "Категорията е задължителна!")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> RecipeTypeItems { get; set; }

        public IEnumerable<SelectListItem> RecipeNationalityItems { get; set; }

        public IEnumerable<SelectListItem> CategoryItems { get; set; }
    }
}
