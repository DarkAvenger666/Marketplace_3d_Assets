namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class AssetCommentEntity
    {
        public Guid AssetCommentId { get; set; }
        public Guid AssetId { get; set; }
        //public AssetEntity? Asset { get; set; }
        public Guid ProfileId { get; set; }
        //public UserProfileEntity? Profile { get; set; }
        public string CommentText { get; set; }
        public DateTime Publicationdate { get; set; }
    }
}
