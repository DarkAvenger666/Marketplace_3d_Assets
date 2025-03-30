using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<bool> RegisterAsync(RegisterViewModel model);
    }
}
