namespace Recipes.InputModels.Recipes
{
    public class EditRecipeInputModel : BaseRecipeInputModel
    {
        public int Id { get; set; }

        public string Photo { get; set; }
    }
}
