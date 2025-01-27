namespace Recipes.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadAsync(IFormFile formFile);
    }
}
