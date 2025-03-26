namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class AssetImageEntity
    {
        public Guid AssetImageId {  get; set; }
        public Guid AssetId { get; set; }
        public string Name { get; set; }
    }
}
