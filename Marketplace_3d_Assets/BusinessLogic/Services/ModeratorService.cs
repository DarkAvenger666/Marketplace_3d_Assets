using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class ModeratorService : IModeratorService
    {
        private readonly ApplicationContext _dbContext;
        //private readonly UserManager<ApplicationUser> _userManager;

        public ModeratorService(ApplicationContext context)
        {
            _dbContext = context;
        }

        public async Task<List<UserListViewModel>> GetAllUsersAsync()
        {
            var users = await _dbContext.UserProfiles
                .Include(p => p.User)
                .Where(p => p.User.Role.Name != "Administrator")
                .Select(p => new UserListViewModel
                {
                    UserId = p.User.User_Id,
                    Name = p.User.Name,
                    UserName = p.User_Name,
                    Email = p.User.Email,
                    Specialization = p.Specialization,
                    Role = p.User.Role.Name
                })
                .ToListAsync();

            return users;
        }

        public async Task ToggleModeratorRoleAsync(Guid userId)
        {
            var user = await _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.User_Id == userId);

            if (user == null)
                throw new Exception("Пользователь не найден");

            if (user.Role == null)
                throw new Exception("У пользователя не задана роль");

            string newRoleName = user.Role.Name == "Moderator" ? "User" : "Moderator";

            var newRole = await _dbContext.UserRoles.FirstOrDefaultAsync(r => r.Name == newRoleName);
            if (newRole == null)
                throw new Exception($"Роль '{newRoleName}' не найдена");

            user.Role_Id = newRole.Role_Id;

            _dbContext.Users.Update(user);

            await _dbContext.SaveChangesAsync();
        }
    }
}
