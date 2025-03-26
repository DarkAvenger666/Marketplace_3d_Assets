namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class PostEntity
    {
        public Guid PostId { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Title { get; set; }
        public string PostText { get; set; }
        public Guid ProfileId { get; set; }
        public int CountOfViews { get; set; }
        public int CountOfLikes { get; set; }
    }
}
