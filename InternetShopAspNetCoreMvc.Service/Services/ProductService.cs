using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;
using InternetShopAspNetCoreMvc.Service.Interfaces;


namespace InternetShopAspNetCoreMvc.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository producRrepository)
        {
            _productRepository = producRrepository;
        }
        public async Task<Product> CreateProductAsync(Product product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                CreatedAt = DateTime.UtcNow,
                CategoryId = product.CategoryId
            };
            await _productRepository.AddProductAsync(newProduct);
            return newProduct;
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            return await _productRepository.EditProductAsync(product);
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }
        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _productRepository.GetAllProductAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }
        public async Task<string> GetImageNameProductAsync(int id)
        {
            return await _productRepository.GetImageNameProductAsync(id);
        }

    }
}

