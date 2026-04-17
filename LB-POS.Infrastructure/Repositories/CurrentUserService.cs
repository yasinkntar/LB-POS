using LB_POS.Infrastructure.Abstracts;
using Microsoft.AspNetCore.Http;

namespace LB_POS.Infrastructure.Repositories
{
    public class CurrentUserService : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserBranchService _userBranchService;

        private List<int>? _branches;

        public CurrentUserService(
            IHttpContextAccessor httpContextAccessor,
            IUserBranchService userBranchService)
        {
            _httpContextAccessor = httpContextAccessor;
            _userBranchService = userBranchService;
        }

        public int UserId =>
            int.Parse(_httpContextAccessor.HttpContext.User.FindFirst("Id").Value);

        public List<int> AllowedBranches =>
            _branches ??= _userBranchService.GetUserBranches(UserId).Result;
    }
}
