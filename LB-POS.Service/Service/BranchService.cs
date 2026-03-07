using LB_POS.Data.Entities;
using LB_POS.Data.Enums;
using LB_POS.Infrastructure.Abstracts;
using LB_POS.Service.IService;
using System.Linq.Expressions;

namespace LB_POS.Service.Service
{
    public class BranchService : IBranchService
    {
        public readonly IBranchRepository repository;
        public BranchService(IBranchRepository _repository)
        {
            repository = _repository;
        }

        public async Task<string> AddAsync(Branch branch)
        {
            await repository.AddAsync(branch);
            return "Success";
        }

        public async Task<string> DeleteAsync(Branch ID)
        {
            await repository.DeleteAsync(ID);
            return "Success";
        }

        public async Task<string> EditAsync(Branch branch)
        {
            await repository.UpdateAsync(branch);
            return "Success";
        }

        public IQueryable<Branch> FilterBranchPaginatedQuerable(BranchOrderingEnum orderingEnum, string search)
        {
            var querable = repository.GetTableNoTracking().AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.Name.Contains(search) || x.Code.Contains(search));
            }
            switch (orderingEnum)
            {
                case BranchOrderingEnum.Code:
                    querable = querable.OrderBy(x => x.Code);
                    break;
                case BranchOrderingEnum.Name:
                    querable = querable.OrderBy(x => x.Name);
                    break;
                case BranchOrderingEnum.ActivityCode:
                    querable = querable.OrderBy(x => x.ActivityCode);
                    break;
                default:
                    querable = querable.OrderBy(x => x.Name);
                    break;
            }

            return querable;
        }

        public async Task<List<Branch>> GetAllBranchesAsync()
        {
            return await repository.GetBranchesAsync();
        }

        public async Task<Branch> GetBrancheByIDAsync(Guid ID)
        {
            return await repository.GetByIdAsync(ID);
        }

        public IQueryable<Branch> GetBranchQuerable()
        {
            return repository.GetTableNoTracking().AsQueryable();
        }

        public async Task<bool> IsUniqueAsync(Expression<Func<Branch, bool>> predicate, Guid? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await repository.IsUniqueAsync(predicate, excludeId, cancellationToken);
        }
    }
}
