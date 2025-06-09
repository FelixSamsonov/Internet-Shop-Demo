 using InternetShopAspNetCoreMvc.Models;

namespace InternetShopAspNetCoreMvc.Repositories.Interfaces
{
	public interface ICategoryRepository
	{
		Task <List<Category>> GetAllCategoryAsync();

		Task <Category?> GetByIdCategoryAsync(int id);

		Task <Category> AddCategoryAsync(Category category);

		Task <bool> DeleteCategoryAsync(int id);

		Task <Category> EditCategoryAsync(Category category);
	}
}
