using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("user")]
    public class UserEntity
    {
        [Key]
        public Guid User_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid Profile_Id { get; set; }
        public string Phone { get; set; }
        public int Role_Id { get; set; }
    }
}
