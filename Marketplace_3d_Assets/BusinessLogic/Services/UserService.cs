using Marketplace_3d_Assets.BusinessLogic.Interfaces;
using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _dbContext;
        public UserService(ApplicationContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<UserEntity?> GetByLoginAsync(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
