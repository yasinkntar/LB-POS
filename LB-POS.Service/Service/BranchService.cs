using LB_POS.Data.Entities;
using LB_POS.Data.Enums;
using LB_POS.Infrastructure.Abstracts;
using LB_POS.Service.IService;
using System.Linq.Expressions;

namespace LB_POS.Service.Service
{
    public class BranchService : IBranchService
    {
        public readonly IBranchRepository _repository;
        public BranchService(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> AddAsync(Branch branch)
        {
            await _repository.AddAsync(branch);
            return "Success";
        }

        public async Task<string> DeleteAsync(Branch ID)
        {
            await _repository.DeleteAsync(ID);
            return "Success";
        }

        public async Task<string> EditAsync(Branch branch)
        {
            await _repository.UpdateAsync(branch);
            return "Success";
        }

        public IQueryable<Branch> FilterBranchPaginatedQuerable(BranchOrderingEnum orderingEnum, string search)
        {
            var querable = _repository.GetTableNoTracking().AsQueryable();
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
            return await _repository.GetBranchesAsync();
        }

        public async Task<Branch> GetBrancheByIDAsync(int ID)
        {
            return await _repository.GetByIdAsync(ID);
        }

        public IQueryable<Branch> GetBranchQuerable()
        {
            return _repository.GetTableNoTracking().AsQueryable();
        }

        public async Task<bool> IsUniqueAsync(Expression<Func<Branch, bool>> predicate, int? excludeId = null, CancellationToken cancellationToken = default)
        {
            return await _repository.IsUniqueAsync(predicate, excludeId, cancellationToken);
        }
    }
}
