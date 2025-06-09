using InternetShopAspNetCoreMvc.Models;
using InternetShopAspNetCoreMvc.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopAspNetCoreMvc.Controllers
{
	public class CartController : Controller
	{
		private readonly ICartRepository _cartRepository;
		private const int UserId = 1;

        public CartController(ICartRepository cartRepository)
		{
			_cartRepository = cartRepository;
		}

		public async Task<IActionResult> Index()
		{
            return View( await _cartRepository.GetUserCartItemsAsync(UserId));
		}

		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(CartItem item)
		{
			item.UserId = UserId;
             await _cartRepository.AddToCartAsync(item);

			return RedirectToAction("Index", "Products");
		}

		public async Task<IActionResult> EmptyCart()
		{
            await _cartRepository.DeleteAllUserCartItemsAsync(UserId);

			return RedirectToAction("Index");
		}

		[HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cartItem = await _cartRepository.GetCartItemAsync(id);

            if (cartItem != null)
            {
                return View(cartItem);
            }

            return View("DoesNotExist");
        }

		[HttpPost]
		public async Task<IActionResult> Edit(CartItem item) 
		{
            await _cartRepository.AddToCartAsync(item);

			return RedirectToAction("Index");
		}

        public async Task<IActionResult> Delete(int id)
		{
			var cartItem = await _cartRepository.GetCartItemAsync(id);

			if (cartItem != null)
			{
               await _cartRepository.DeleteUserCartItemAsync(cartItem.Id);
			}

			return RedirectToAction("Index");
		}
	}
}
