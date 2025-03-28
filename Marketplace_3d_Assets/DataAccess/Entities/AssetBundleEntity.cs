using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("asset_bundle")]
    public class AssetBundleEntity
    {
        [Key]
        public Guid Asset_Bundle_Id { get; set; }
        public Guid Asset_Id { get; set; }
        //public AssetEntity? Asset { get; set; }
        public Guid Profile_Id { get; set; }
        //public UserProfileEntity? Profile { get; set; }
        public string Title { get; set; }
        public string Asset_Bundle_Description { get; set; }
        public DateTime Creation_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public int Discount_Percaentage { get; set; }
    }
}
