using Microsoft.AspNetCore.Http;

namespace InternetShopAspNetCoreMvc.ViewModels
{
    public interface IProductViewModelImage
    {
        IFormFile Image { get; set; }
    }
}
