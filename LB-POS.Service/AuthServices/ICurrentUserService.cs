using LB_POS.Data.Entities.Identity;

namespace LB_POS.Service.AuthServices
{
    public interface ICurrentUserService
    {
        public Task<User> GetUserAsync();
        public int GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
