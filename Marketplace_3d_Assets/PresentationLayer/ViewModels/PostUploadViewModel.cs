namespace Marketplace_3d_Assets.PresentationLayer.ViewModels
{
    public class PostUploadViewModel
    {
        public string Title { get; set; }
        public string Post_Text { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
