namespace Marketplace_3d_Assets.PresentationLayer.ViewModels
{
    public class ModerationDetailsViewModel
    {
        public Guid RequestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author_Name { get; set; }
        public string Type_Name { get; set; }
        public List<string> Images { get; set; }
        public List<string> Tags { get; set; }
        public float Price { get; set; }
    }
}
