using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using System.Text.Json;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class CookieCartService : ICartService
    {
        private const string CartCookieKey = "Cart";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieCartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItemViewModel> GetCartItems()
        {
            var cookie = _httpContextAccessor.HttpContext.Request.Cookies[CartCookieKey];
            return string.IsNullOrEmpty(cookie)
                ? new List<CartItemViewModel>()
                : JsonSerializer.Deserialize<List<CartItemViewModel>>(cookie);
        }

        public void AddToCart(CartItemViewModel item)
        {
            var items = GetCartItems();
            var existingItem = items.FirstOrDefault(i => i.Asset_Id == item.Asset_Id);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                items.Add(item);
            }
            Save(items);
        }

        public void RemoveFromCart(Guid assetId)
        {
            var items = GetCartItems().Where(i => i.Asset_Id != assetId).ToList();
            Save(items);
        }

        public void ClearCart()
        {
            Save(new List<CartItemViewModel>());
        }

        private void Save(List<CartItemViewModel> items)
        {
            var json = JsonSerializer.Serialize(items);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CartCookieKey, json, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7)
            });
        }
    }
}
