using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    [Table("user_role")]
    public class UserRoleEntity
    {
        [Key]
        public int Role_Id { get; set; }
        public string Name { get; set; }
    }
}
