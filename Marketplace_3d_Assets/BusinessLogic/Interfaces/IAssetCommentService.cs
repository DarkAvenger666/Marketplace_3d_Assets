using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.PresentationLayer.DTOs;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IAssetCommentService
    {
        Task<IEnumerable<CommentViewModel>> GetCommentsAsync(Guid assetId);
        Task<CommentsDto> GetCommentsByAssetIdPaged(Guid assetId, int page = 1, int pageSize = 10);
        Task<CommentViewModel> CreateCommentAsync(CreateCommentDto commentDto, string authorUserName);
        Task<bool> DeleteCommentAsync(Guid commentId);
    }
}
