namespace Marketplace_3d_Assets.PresentationLayer.ViewModels.Filters
{
    public class AssetFilterModel
    {
        public int? TypeId { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public string? SearchQuery { get; set; }
        public string? SortBy { get; set; }
        public bool SortDescending { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
