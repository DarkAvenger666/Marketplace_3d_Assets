using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("post_image")]
    public class PostImageEntity
    {
        [Key]
        public Guid Post_Image_Id { get; set; }
        public Guid Post_Id { get; set; }
        public string Name { get; set; }
    }
}
