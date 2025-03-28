using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("asset_image")]
    public class AssetImageEntity
    {
        [Key]
        public Guid Asset_Image_Id {  get; set; }
        public Guid Asset_Id { get; set; }
        public string Name { get; set; }
    }
}
