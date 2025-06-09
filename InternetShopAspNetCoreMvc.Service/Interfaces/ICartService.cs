using InternetShopAspNetCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopAspNetCoreMvc.Service.Interfaces
{
    public interface ICartService
    {
        Task<CartItem> GetCartItemAsync(int id);

        Task<IEnumerable<CartItem>> GetUserCartItemsAsync(int id);

        Task<IEnumerable<CartItem>> AddToCartAsync(CartItem item);

        Task DeleteUserCartItemAsync(CartItem cartItem);

        Task DeleteAllUserCartItemsAsync(int userId);

        Task UpdateCartItemAsync(CartItem item);
    }
}
