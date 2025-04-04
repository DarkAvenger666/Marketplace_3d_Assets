using Marketplace_3d_Assets.PresentationLayer.ViewModels;

namespace Marketplace_3d_Assets.PresentationLayer.DTOs
{
    public class CommentsDto
    {
        public IEnumerable<CommentViewModel> Comments { get; set; }
        public int totalCount { get; set; }
    }
}
