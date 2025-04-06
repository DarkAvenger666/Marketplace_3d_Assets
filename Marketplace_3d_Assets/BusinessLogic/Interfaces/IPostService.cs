using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Marketplace_3d_Assets.PresentationLayer.ViewModels.Filters;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IPostService
    {
        Task<Guid> CreatePost(PostUploadViewModel postViewModel, Guid ProfileId);
        Task<List<PostCardViewModel>> GetPostsForPostPage();
        Task<(List<PostCardViewModel> Posts, int TotalCount)> GetFilteredPostsAsync(PostFilterViewModel filter);
        Task<PostDetailsViewModel> GetPostDetailsAsync(Guid postId);

    }
}
