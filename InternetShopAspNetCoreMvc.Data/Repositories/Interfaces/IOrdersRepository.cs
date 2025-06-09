using InternetShopAspNetCoreMvc.Models;

namespace InternetShopAspNetCoreMvc.Repositories.Interfaces
{
	public interface IOrdersRepository
	{
		Task <List<Order>> GetUserOrdersAsync(int id);

		Task <List<Order>> GetUserOrdersWithDetailsAsync(int id);

		Task <Order?> GetOrderWithDetailsAsync(int id);

		Task <List<Order>> GetAllOrdersAsync();
        Task<int> CreateOrderAsync(Order order);
        Task AddOrderDetailAsync(OrderDetail detail);

    }
}
