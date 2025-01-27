using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Recipes.Common;
using Recipes.Interfaces;

namespace Recipes.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly IConfiguration configuration;
        private readonly CloudinarySettings cloudinarySettings;
        private readonly Cloudinary cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.cloudinarySettings = this.configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();

            Account account = new Account(
                this.cloudinarySettings.CloudName,
                this.cloudinarySettings.ApiKey,
                this.cloudinarySettings.ApiSecret);

            this.cloudinary = new Cloudinary(account);
        }

        public async Task<string> UploadAsync(IFormFile formFile)
        {
            var uploadResult = new ImageUploadResult();

            if (formFile.Length > 0)
            {
                using (var stream = formFile.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(formFile.Name, stream)
                    };

                    uploadResult = await this.cloudinary.UploadAsync(uploadParams);
                }
            }

            return uploadResult.SecureUrl.ToString();
        }
    }
}
