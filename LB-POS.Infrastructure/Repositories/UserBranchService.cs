using LB_POS.Infrastructure.Abstracts;
using LB_POS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LB_POS.Infrastructure.Repositories
{
    public class UserBranchService : IUserBranchService
    {
        private readonly ApplicationDBContext _dbContext;

        public UserBranchService(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<int>> GetUserBranches(int userId)
        {
            return await _dbContext.UserBranches
                .Where(x => x.UserId == userId)
                .Select(x => x.BranchId).ToListAsync();
        }
    }
}
