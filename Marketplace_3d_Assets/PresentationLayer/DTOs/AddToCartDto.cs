namespace Marketplace_3d_Assets.PresentationLayer.DTOs
{
    public class AddToCartDto
    {
        public Guid assetId { get; set; } 
        public string title { get; set; }
        public float price { get; set; }
    }
}
