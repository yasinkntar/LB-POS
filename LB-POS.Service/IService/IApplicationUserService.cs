using LB_POS.Data.Entities.Identity;

namespace LB_POS.Service.IService
{
    public interface IApplicationUserService
    {
        public Task<string> AddUserAsync(User user, string password);
    }
}
