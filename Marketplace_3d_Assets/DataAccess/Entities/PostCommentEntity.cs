using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("post_comment")]
    public class PostCommentEntity
    {
        [Key]
        public Guid Post_Comment_Id { get; set; }
        public Guid Post_Id { get; set; }
        public Guid Profile_Id { get; set; }
        public string Comment_Text { get; set; }
        public DateTime Publication_Date { get; set; }
        public UserProfileEntity Profile { get; set; }
    }
}
