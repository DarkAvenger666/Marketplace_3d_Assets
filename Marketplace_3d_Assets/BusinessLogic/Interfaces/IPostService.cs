using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.BusinessLogic.Interfaces
{
    public interface IPostService
    {
        Task<Guid> CreatePost(PostUploadViewModel postViewModel, Guid ProfileId);
        Task<List<PostCardViewModel>> GetPostsForPostPage();
        Task<PostDetailsViewModel> GetPostDetailsAsync(Guid postId);

    }
}
