namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class PostCommentEntity
    {
        public Guid PostCommentId { get; set; }
        public Guid PostId { get; set; }
        public Guid ProfileId { get; set; }
        public string CommentText { get; set; }
        public DateTime Publicationdate { get; set; }
    }
}
