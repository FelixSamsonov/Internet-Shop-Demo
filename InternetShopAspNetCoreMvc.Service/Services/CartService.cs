using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Service.Interfaces;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;

namespace InternetShopAspNetCoreMvc.Service.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<CartItem>> AddToCartAsync(CartItem item)
        {
            var existing = (await _cartRepository
                .GetUserCartItemsAsync(item.UserId))
                .FirstOrDefault(ci => ci.ProductId == item.ProductId);

            if (existing != null)
            {
                existing.Quantity += item.Quantity;
                await _cartRepository.EditCartItemAsync(existing);
            }
            else
            {
                await _cartRepository.AddToCartAsync(item);
            }

            return await _cartRepository.GetUserCartItemsAsync(item.UserId);
        }


        public async Task DeleteAllUserCartItemsAsync(int userId)
        {
            await _cartRepository.DeleteAllUserCartItemsAsync(userId);
        }

        public async Task DeleteUserCartItemAsync(CartItem cartItem)
        {
            await _cartRepository.DeleteUserCartItemAsync(cartItem.Id);
        }

        public async Task<CartItem> GetCartItemAsync(int id)
        {
            return await _cartRepository.GetCartItemAsync(id);
        }

        public async Task<IEnumerable<CartItem>> GetUserCartItemsAsync(int userId)
        {
            return await _cartRepository.GetUserCartItemsAsync(userId);
        }

        public async Task UpdateCartItemAsync(CartItem item)
        {
            await _cartRepository.EditCartItemAsync(item);
        }
    }
}
