using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("post")]
    public class PostEntity
    {
        [Key]
        public Guid Post_Id { get; set; }
        public DateTime Publication_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public string Title { get; set; }
        public string Post_Text { get; set; }
        public Guid Profile_Id { get; set; }
        public int Count_Of_Views { get; set; }
        public int Count_Of_Likes { get; set; }
    }
}
