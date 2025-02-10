namespace Recipes.ViewModels.Recipes
{
    public class RecipeDetailsViewModel : RecipeViewModel
    {
        public string GetDifficulty()
        {
            if (this.Difficulty <= 3)
            {
                return $"{this.Difficulty} (Лесно)";
            }
            else if (this.Difficulty <= 6)
            {
                return $"{this.Difficulty} (Средно)";
            }
            else if (this.Difficulty <= 8)
            {
                return $"{this.Difficulty} (Трудно)";
            }
            else
            {
                return $"{this.Difficulty} (Много трудно)";
            }
        }
    }
}
