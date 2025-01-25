using System.ComponentModel.DataAnnotations;

namespace Recipes.InputModels.Categories
{
    public class BaseCategoryInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
