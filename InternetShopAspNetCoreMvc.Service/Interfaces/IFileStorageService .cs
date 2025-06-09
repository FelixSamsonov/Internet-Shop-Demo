using Microsoft.AspNetCore.Http;


namespace InternetShopAspNetCoreMvc.Service.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveAsync(IFormFile file, string subfolder);
        Task DeleteAsync(string relativeUrl);
    }
}
