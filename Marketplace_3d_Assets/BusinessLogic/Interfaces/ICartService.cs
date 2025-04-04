using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface ICartService
    {
        List<CartItemViewModel> GetCartItems();
        void AddToCart(CartItemViewModel item);
        void RemoveFromCart(Guid assetId);
        void ClearCart();
    }
}
