using Marketplace_3d_Assets.Data;
using Marketplace_3d_Assets.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Marketplace_3d_Assets.DataAccess.Repositories
{
    public class ModerationRepository : IModerationRepository
    {
        private readonly ApplicationContext _context;

        public ModerationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> GetUserWithMinModerationRequestsAsync()
        {
            var userRequestCounts = await _context.Users
                .Where(u => u.Role_Id == 2)
                .GroupJoin(
                    _context.ModerationRequests,
                    user => user.User_Id,
                    request => request.User_Id,
                    (user, requests) => new
                    {
                        User = user,
                        RequestCount = requests.Count()
                    })
                .OrderBy(x => x.RequestCount)
                .Select(x => x.User)
                .FirstOrDefaultAsync();

            if (userRequestCounts == null) throw new Exception("Произошла ошибка");

            return userRequestCounts.User_Id;
        }
    }
}
