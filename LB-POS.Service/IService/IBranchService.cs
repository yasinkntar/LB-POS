using LB_POS.Data.Entities;
using LB_POS.Data.Enums;
using System.Linq.Expressions;

namespace LB_POS.Service.IService
{
    public interface IBranchService
    {
        Task<List<Branch>> GetAllBranchesAsync();
        Task<Branch> GetBrancheByIDAsync(Guid ID);
        Task<string> AddAsync(Branch branch);
        Task<string> EditAsync(Branch branch);
        Task<string> DeleteAsync(Branch ID);
        Task<bool> IsUniqueAsync(Expression<Func<Branch, bool>> predicate, Guid? excludeId = null, CancellationToken cancellationToken = default);
        public IQueryable<Branch> GetBranchQuerable();
        //public IQueryable<Branch> GetBranchByDepartmentIDQuerable(int DID);
        public IQueryable<Branch> FilterBranchPaginatedQuerable(BranchOrderingEnum orderingEnum, string search);


    }
}
