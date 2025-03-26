using System.ComponentModel.DataAnnotations;

namespace Marketplace_3d_Assets.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid ProfileId { get; set; }
        public string Phone { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
