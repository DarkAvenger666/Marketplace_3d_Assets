using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("asset_file")]
    public class AssetFileEntity
    {
        [Key]
        public Guid Asset_File_Id { get; set; }
        public Guid Asset_Id { get; set; }
        public int File_Type_Id { get; set; }
        public string File_Name { get; set; }
    }
}
