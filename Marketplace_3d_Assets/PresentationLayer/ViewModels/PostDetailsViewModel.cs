namespace Marketplace_3d_Assets.PresentationLayer.ViewModels
{
    public class PostDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Post_Text { get; set; }
        public DateTime Publication_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public string Author_Name { get; set; }
        public int Count_Of_Views { get; set; }
        public List<string> Tags { get; set; }
    }
}
