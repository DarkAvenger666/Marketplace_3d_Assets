namespace Marketplace_3d_Assets.PresentationLayer.ViewModels
{
    public class AssetCardViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author_Name { get; set; }
        public string Type_Name { get; set; }
        public int Count_Of_Copies_Sold { get; set; }
        public string Asset_Image { get; set; }
        public float Price { get; set; }
        public int Count_Of_Comments { get; set; }
        public int Count_Of_Views { get; set; }
        public int Count_Of_Likes { get; set; }
    }
}
