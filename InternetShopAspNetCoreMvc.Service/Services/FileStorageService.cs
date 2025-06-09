using InternetShopAspNetCoreMvc.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace InternetShopAspNetCoreMvc.Service.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> SaveAsync(IFormFile file, string subfolder)
        {
            if (file == null || file.Length == 0)
                return $"/{subfolder}/no-image.jpg";

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, subfolder.Trim('/'));
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await using var fs = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fs);

            return $"/{subfolder.Trim('/')}/{uniqueFileName}";
        }
        public Task DeleteAsync(string relativeUrl)
        {
            if (string.IsNullOrWhiteSpace(relativeUrl))
                return Task.CompletedTask;

            var trimmed = relativeUrl.TrimStart('/');
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath,
                trimmed.Replace('/', Path.DirectorySeparatorChar));

            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }
    }
}
