namespace Marketplace_3d_Assets.PresentationLayer.ViewModels
{
    public class CartItemViewModel
    {
        public Guid Asset_Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
