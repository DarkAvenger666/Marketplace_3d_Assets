namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class AssetBundleEntity
    {
        public Guid AssetBundleId { get; set; }
        public Guid AssetId { get; set; }
        //public AssetEntity? Asset { get; set; }
        public Guid ProfileId { get; set; }
        //public UserProfileEntity? Profile { get; set; }
        public string Title { get; set; }
        public string AssetBundleDescription { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int DiscountPercaentage { get; set; }
    }
}
