using InternetShopAspNetCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopAspNetCoreMvc.Service.Interfaces
{
    public interface IProductService
    {
        Task <Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<string> GetImageNameProductAsync(int id);
    }
}
