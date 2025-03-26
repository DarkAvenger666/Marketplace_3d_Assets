namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class AssetEntity
    {
        public Guid AssetId { get; set; }
        public string Title { get; set; }
        public int TypeId { get; set; }
        //public AssetTypeEntity? Type { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int StatusId { get; set; }
        //public AssetStatusEntity? Status { get; set; }
        public int CountOfCopiesSold { get; set; }
        public Guid ProfileId { get; set; }
        //public UserProfileEntity? UserProfile { get; set; }
        public string AssetDescription { get; set; }
        public float Price { get; set; }
        public int CountOfViews { get; set; }
        public int CountOfLikes { get; set; }
    }
}
