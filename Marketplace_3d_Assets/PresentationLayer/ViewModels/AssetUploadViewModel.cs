namespace Marketplace_3d_Assets.PresentationLayer.ViewModels
{
    public class AssetUploadViewModel
    {
        public Guid AssetId { get; set; }
        public string Title { get; set; }
        public string AssetDescription { get; set; }
        public int TypeId { get; set; }
        public Guid ProfileId { get; set; }
        public float Price { get; set; }
        public List<IFormFile> Images { get; set; } = new();
        public List<IFormFile> Files { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }
}
