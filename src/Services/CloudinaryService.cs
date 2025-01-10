using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;

namespace Cat3_API.Src.Services
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration configuration)
        {
            var cloudinarySettings = configuration.GetSection("CloudinarySettings");
            _cloudinary = new Cloudinary(new Account(
                cloudinarySettings["CloudName"],
                cloudinarySettings["ApiKey"],
                cloudinarySettings["ApiSecret"]));
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            if (file.Length == 0)
                throw new Exception("El archivo está vacío.");

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Transformation = new Transformation().Quality(80).Crop("limit").Width(800).Height(800)
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.Error != null)
                throw new Exception($"Error al subir la imagen: {uploadResult.Error.Message}");

            return uploadResult.SecureUrl.ToString();
        }
    }
}
