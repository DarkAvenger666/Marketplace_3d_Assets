namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class AssetFileEntity
    {
        public Guid AssetFileId { get; set; }
        public Guid AssetId { get; set; }
        public int FileTypeId { get; set; }
        public string FileName { get; set; }
    }
}
