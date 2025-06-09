using InternetShopAspNetCoreMvc.Data;
using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternetShopAspNetCoreMvc.Repositories
{
	public class OrdersRepository : IOrdersRepository
	{
		private readonly InternetShopDbContext _context;

		public OrdersRepository(InternetShopDbContext context)
		{
			_context = context;
		}

		public async Task<List<Order>> GetAllOrdersAsync()
		{
            return await _context.Orders
				.AsNoTracking()
				.ToListAsync();
        }

		public async Task <Order?> GetOrderWithDetailsAsync(int id)
		{
			return await _context.Orders
				.AsNoTracking()
				.Include(x => x.OrderDetails)
				.ThenInclude(x => x.Product)
				.FirstOrDefaultAsync(x => x.Id == id);
        }

		public async Task <List<Order>> GetUserOrdersAsync(int id)
		{
			return await _context.Orders
				.AsNoTracking()
				.Where(x => x.UserId == id).ToListAsync();
		}

		public async Task <List<Order>> GetUserOrdersWithDetailsAsync(int id)
		{
            return await _context.Orders
				.AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Product)
				.Where(x => x.UserId == id)
				.ToListAsync();
        }
        public async Task<int> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }
        public async Task AddOrderDetailAsync(OrderDetail detail)
        {
            _context.OrderDetails.Add(detail);
            await _context.SaveChangesAsync();
        }
    }
}
