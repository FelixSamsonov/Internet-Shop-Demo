using InternetShopAspNetCoreMvc.Data;
using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAspNetCoreMvc.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly InternetShopDbContext _context;

		public CategoryRepository(InternetShopDbContext context)
		{
			_context = context;
		}

        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == category.Id)
                ?? throw new KeyNotFoundException("Category not found");
        }
        public async Task<Category?> GetByIdCategoryAsync(int id)
        {
            return await _context.Categories.AsNoTracking().Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id)
                ?? throw new KeyNotFoundException("Category not found");
        }

        public async Task <bool> DeleteCategoryAsync(int id)
        {
            var category = await GetByIdCategoryAsync(id);

            if(category == null) 
                return false;

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return true;    
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> EditCategoryAsync(Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Description = category.Description;

                await _context.SaveChangesAsync();
            }

            return existingCategory;
        }
    }
}
