using InternetShopAspNetCoreMvc.Data;
using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Repositories;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;
using InternetShopAspNetCoreMvc.Service.Interfaces;
using Microsoft.Extensions.Logging;

public class OrderService : IOrderService
{
    private readonly ICartRepository _cartRepository;
    private readonly IOrdersRepository _orderRepository;
    private readonly InternetShopDbContext _context;
    private readonly ILogger<OrderService> _logger;

    public OrderService(ICartRepository cartRepository, IOrdersRepository orderRepository, InternetShopDbContext context, ILogger<OrderService> logger)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
        _context = context;
        _logger = logger;
    }

    public async Task ConfirmOrderAsync(int userId)
    {
        var cartItems = await _cartRepository.GetUserCartItemsAsync(userId);

        if (cartItems != null && cartItems.Count > 0)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var totalWithNoTax = cartItems.Select(c => c.Product.Price * c.Quantity).Sum();
                var order = new Order
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    Amount = totalWithNoTax
                };
                var newOrderId = await _orderRepository.CreateOrderAsync(order);
                //_context.Orders.Add(order);
                //await _context.SaveChangesAsync();

                foreach (var item in cartItems)
                {
                    var orderDetails = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = item.ProductId,
                        Price = item.Product.Price,
                        Quantity = item.Quantity,
                        Total = item.Quantity * item.Product.Price,
                    };
                    await _orderRepository.AddOrderDetailAsync(orderDetails);
                    await _cartRepository.DeleteUserCartItemAsync(item.Id);
                }
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Помилка підтвердження замовлення користувача {UserId}", userId);
                throw;
            }
        }
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllOrdersAsync();
    }

    public async Task<Order?> GetOrderWithDetailsAsync(int id)
    {
        return await _orderRepository.GetOrderWithDetailsAsync(id);
    }

    public async Task<IEnumerable<Order>> GetUserOrdersAsync(int id)
    {
        return await _orderRepository.GetUserOrdersAsync(id);
    }

    public async Task<IEnumerable<Order>> GetUserOrdersWithDetailsAsync(int id)
    {
        return await _orderRepository.GetUserOrdersWithDetailsAsync(id);
    }
}

