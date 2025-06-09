using InternetShopAspNetCoreMvc.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopAspNetCoreMvc.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
       private const int UserId = 1;

        public OrdersController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            const int UserId = 1;
            var orders = await _orderService.GetUserOrdersWithDetailsAsync(UserId);
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderWithDetailsAsync(id);
            if (order == null)
            {
                return View("DoesNotExist");
            }
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> PlaceOrder()
        {
            const int UserId = 1;
            var cartItems = await _cartService.GetUserCartItemsAsync(UserId); 
            ViewData["Total"] = 0;
            return View();
        }

        [HttpPost, ActionName("PlaceOrderConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrderConfirmed()
        {
            const int UserId = 1;
            await _orderService.ConfirmOrderAsync(UserId);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Manage()
        {
            var allOrders = await _orderService.GetAllOrdersAsync();
            return View(allOrders);
        }
    }
}
