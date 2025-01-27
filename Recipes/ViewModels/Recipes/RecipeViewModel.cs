using System.ComponentModel.DataAnnotations;

namespace Recipes.ViewModels.Recipes
{
    public class RecipeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int PreparationTime { get; set; }

        public int CookingTime { get; set; }
        
        public int Portions { get; set; }
        
        public int Difficulty { get; set; }

        public string Photo { get; set; }

        public int RecipeTypeId { get; set; }

        public string RecipeTypeName { get; set; }

        public int RecipeNationalityId { get; set; }

        public string RecipeNationalityName { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }


    }
}
