using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class CheckoutServiceStub : ICheckoutService
    {
        public async Task<bool> PurchaseAsync(List<CartItemViewModel> cartItems, Guid userId)
        {
            
            await Task.Delay(1000);
            Console.WriteLine($"Пользователь {userId} купил {cartItems.Count} товаров.");
            return true;
        }
    }
}
