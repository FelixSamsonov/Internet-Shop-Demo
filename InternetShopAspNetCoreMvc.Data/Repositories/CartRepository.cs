using InternetShopAspNetCoreMvc.Data;
using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAspNetCoreMvc.Repositories
{
	public class CartRepository : ICartRepository
	{
		private readonly InternetShopDbContext _context;

		public CartRepository(InternetShopDbContext context)
		{
			_context = context;
		}

        public async Task AddToCartAsync(CartItem item)
        {
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.UserId == item.UserId && c.ProductId == item.ProductId);

            if (existingItem != null)
            {
                 existingItem.Quantity += item.Quantity;
            }
            else
            {
                var product = await _context.Products.FirstOrDefaultAsync(c => c.Id == item.ProductId);
                item.Product = product;

                await _context.CartItems.AddAsync(item);
            }

            await _context.SaveChangesAsync();
        }

        public Task<List<CartItem>> GetUserCartItemsAsync(int id)
        {
            return  _context.CartItems.AsNoTracking().Include(c => c.Product).Where(c => c.UserId == id).ToListAsync();
        }

        public async Task DeleteUserCartItemAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAllUserCartItemsAsync(int userId)
        {
           await _context.CartItems.Where(c => c.UserId == userId).ExecuteDeleteAsync();
        }

        public async Task <CartItem> GetCartItemAsync(int id)
        {
            return await _context.CartItems.AsNoTracking().Include(c => c.Product).FirstOrDefaultAsync(c => c.Id == id) 
                ?? throw new InvalidOperationException("Product not found");
        }

        public async Task EditCartItemAsync(CartItem item)
        {
            var itemToEdit = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == item.Id);

            if (itemToEdit != null)
            {
                itemToEdit.Quantity = item.Quantity;
                await _context.SaveChangesAsync();
            }
        }
    }
}
