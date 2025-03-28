using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("file_type")]
    public class FileTypeEntity
    {
        [Key]
        public int File_Type_Id { get; set; }
        public string Name { get; set; }
    }
}
