using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface ICheckoutService
    {
        Task<bool> PurchaseAsync(List<CartItemViewModel> cartItems, Guid userId);
    }
}
