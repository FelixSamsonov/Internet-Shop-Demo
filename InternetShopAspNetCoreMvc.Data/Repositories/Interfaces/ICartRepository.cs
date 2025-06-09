using InternetShopAspNetCoreMvc.Models;

namespace InternetShopAspNetCoreMvc.Repositories.Interfaces
{
	public interface ICartRepository
	{
		Task <CartItem> GetCartItemAsync(int id);

		Task<List<CartItem>> GetUserCartItemsAsync(int id);

		Task AddToCartAsync(CartItem item);

		Task DeleteUserCartItemAsync(int cartItemId);

        Task DeleteAllUserCartItemsAsync(int userId);

        Task EditCartItemAsync(CartItem item);
	}
}
