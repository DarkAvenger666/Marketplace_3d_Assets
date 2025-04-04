using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("post_tag")]
    public class PostTagEntity
    {
        public Guid Post_Id { get; set; }
        public int Tag_Id { get; set; }
        public TagEntity Tag { get; set; }
        public PostEntity Post { get; set; }
    }
}
