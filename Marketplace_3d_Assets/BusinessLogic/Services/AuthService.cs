using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Entities;
using Marketplace_3d_Assets.PresentationLayer.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Security.Claims;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Marketplace_3d_Assets.BusinessLogic.Interfaces;

namespace Marketplace_3d_Assets.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserProfileRepository _userProfileRep;
        public AuthService(ApplicationContext dbContext, IHttpContextAccessor httpContextAccessor,
                           IUserProfileRepository userProfileRep)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
            _userProfileRep = userProfileRep;
        }
        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            var user = await _dbContext.Users.Include(u => u.Profile)
                         .Include(u => u.Role)
                         .FirstOrDefaultAsync(u => u.Email == model.Login || u.Profile.User_Name == model.Login);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password_Hash))
                return false;

            if (user.Profile == null || user.Role == null)
                throw new Exception("Связанные данные не найдены!");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.User_Id.ToString()),
                new Claim("ProfileId", user.Profile.Profile_Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Profile.User_Name ?? "Unknown"),
                new Claim(ClaimTypes.Role, user.Role.Name ?? "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await _httpContextAccessor.HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);
            return true;
        }
        public async Task LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync("MyCookieAuth");
        }
        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Email == model.Email))
                return false;

            if (await _dbContext.UserProfiles.AnyAsync(u => u.User_Name == model.User_Name))
                return false;


            var profileId = Guid.NewGuid();
            var profile = new UserProfileEntity
            {
                Profile_Id = profileId,
                User_Name = model.User_Name,
                Subscribers_Count = 0,
                Subscriptions_Count = 0,
                Gender = model.Gender,
                Specialization = model.Specialization,
                City = model.City,
                About = model.About,
            };
            await _userProfileRep.AddAsync(profile);

            var user = new UserEntity
            {
                User_Id = Guid.NewGuid(),
                Name = model.Name,
                Email = model.Email,
                Password_Hash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Profile_Id = profileId,
                Phone = model.PhoneNumber,
                Role_Id = 1
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
