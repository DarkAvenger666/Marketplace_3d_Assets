using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IModeratorService
    {
        Task<List<UserListViewModel>> GetAllUsersAsync();
        Task ToggleModeratorRoleAsync(Guid userId);
    }
}
