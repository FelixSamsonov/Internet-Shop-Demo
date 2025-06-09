using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;
using InternetShopAspNetCoreMvc.Service.Interfaces;


namespace InternetShopAspNetCoreMvc.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            var newCategory = new Category
            {
                Name = category.Name,
                Description = category.Description,
                CreatedAt = DateTime.UtcNow,
            };
            await _categoryRepository.AddCategoryAsync(newCategory);
            return newCategory;
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            return await _categoryRepository.EditCategoryAsync(category);
        }
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteCategoryAsync(id);
        }
        public async Task<IEnumerable<Category>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAllCategoryAsync();
        }
        public async Task<Category?> GetByIdCategoryAsync(int id)
        {
            return await _categoryRepository.GetByIdCategoryAsync(id);            
        }
    }
}
