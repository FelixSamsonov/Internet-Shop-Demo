using InternetShopAspNetCoreMvc.Models;

namespace InternetShopAspNetCoreMvc.Repositories.Interfaces
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAllProductAsync();

        Task<Product> GetProductByIdAsync(int id);

        Task<Product> AddProductAsync(Product product);

        Task<Product> EditProductAsync(Product product);

        Task<bool> DeleteProductAsync(int id);

        Task<string> GetImageNameProductAsync(int id);
	}
}
