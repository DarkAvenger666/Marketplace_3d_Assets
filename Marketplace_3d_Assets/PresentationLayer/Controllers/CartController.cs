using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.PresentationLayer.DTOs;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Marketplace_3d_Assets.PresentationLayer.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;

        public CartController(ICartService cartService, ICheckoutService checkoutService)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
        }

        public IActionResult Index()
        {
            var items = _cartService.GetCartItems();
            return View(items);
        }

        [AllowAnonymous]
        public IActionResult Add([FromBody] AddToCartDto dtoItem)
        {
            Console.WriteLine($"{dtoItem.assetId} {dtoItem.title} {dtoItem.price}");
            var item = new CartItemViewModel
            {
                Asset_Id = dtoItem.assetId,
                Title = dtoItem.title,
                Price = dtoItem.price,
                Quantity = 1
            };

            _cartService.AddToCart(item);
            return Ok();
        }

        public IActionResult Remove(Guid id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Checkout()
        {
            var items = _cartService.GetCartItems();
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            await _checkoutService.PurchaseAsync(items, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            _cartService.ClearCart();
            return RedirectToAction("Index", "Home");
        }
    }
}
