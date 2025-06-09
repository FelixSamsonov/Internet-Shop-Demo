using InternetShopAspNetCoreMvc.Data;
using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAspNetCoreMvc.Repositories
{
	public class ProductRepository : IProductRepository
	{
		private readonly InternetShopDbContext _context;

		public ProductRepository(InternetShopDbContext context) 
		{
			_context = context;
		}

		public async Task<Product>AddProductAsync(Product product)
		{
			await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
			return product;
		}

		public async Task<bool> DeleteProductAsync(int id)
		{
			var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

			if (product != null)
			{
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
			}
			return true;
		}

		public async Task<Product> EditProductAsync(Product product)
		{
			var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

			if (existingProduct != null)
			{
				existingProduct.Name = product.Name;
				existingProduct.Description = product.Description;
				existingProduct.Price = product.Price;
				existingProduct.CategoryId = product.CategoryId;
				existingProduct.Image = product.Image;
                await _context.SaveChangesAsync();
			}

			return existingProduct ?? throw new KeyNotFoundException("Product not found"); 
		}

		public async Task<List<Product>> GetAllProductAsync()
		{
			// AsNoTracking -> если вы не будете делать изменения сущности из базы - стоит использовать эту функцию 
			// и ef core не будет следить за изменениями объекта
			// для GET
			return await _context.Products.AsNoTracking().ToListAsync();
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _context.Products
				.AsNoTracking()
				.Include(x => x.Category)
				.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new KeyNotFoundException("Product not found");
        }

		public async Task<string> GetImageNameProductAsync(int id)
		{
			var imageName = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            return imageName?.Name ?? throw new KeyNotFoundException("Product not found");
        }
	}
}
