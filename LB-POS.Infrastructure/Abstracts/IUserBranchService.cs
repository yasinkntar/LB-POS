namespace LB_POS.Infrastructure.Abstracts
{
    public interface IUserBranchService
    {
        Task<List<int>> GetUserBranches(int userId);
    }
}
