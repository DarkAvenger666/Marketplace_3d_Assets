namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class CartItemEntity
    {
        public Guid CartItemId { get; set; }
        public Guid UserId { get; set; }
        public Guid AssetId { get; set; }
        public Guid AssetBundleId { get; set; }
    }
}
