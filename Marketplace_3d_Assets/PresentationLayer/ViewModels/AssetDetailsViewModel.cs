namespace Marketplace_3d_Assets.PresentationLayer.ViewModels
{
    public class AssetDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author_Name { get; set; }
        public string Type_Name { get; set; }
        public DateTime Upload_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Count_Of_Copies_Sold { get; set; }
        public List<string> Images { get; set; }
        public List<string> Tags { get; set; }
        public float Price { get; set; }
        public int Count_Of_Comments { get; set; }
        public int Count_Of_Views { get; set; }
        public int Count_Of_Likes { get; set; }
    }
}
