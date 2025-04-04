namespace Marketplace_3d_Assets.BusinessLogic.Models
{
    public class AssetCardDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author_Name { get; set; }
        public string Type_Name { get; set; }
        public int Count_Of_Copies_Sold { get; set; }
        public Guid Asset_Image_Id { get; set; }
        public float Price { get; set; }
        public int Count_of_Comments { get; set; }
        public int Count_Of_Views { get; set; }
        public int Count_Of_Likes { get; set; }
    }
}
