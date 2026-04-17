namespace LB_POS.Infrastructure.Abstracts
{
    public interface ICurrentUser
    {
        int UserId { get; }
        List<int> AllowedBranches { get; }
    }
}
