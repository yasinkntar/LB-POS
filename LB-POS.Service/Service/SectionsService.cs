using LB_POS.Data.Entities;
using LB_POS.Infrastructure.Abstracts;
using LB_POS.Service.IService;
using System.Linq.Expressions;

namespace LB_POS.Service.Service
{
    public class SectionsService : ISectionsService
    {
        public readonly ISectionsRepository _repository;
        public SectionsService(ISectionsRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> AddAsync(Section branch)
        {
            await _repository.AddAsync(branch);
            return "Messages.Added";
        }

        public async Task<string> DeleteAsync(Section ID)
        {
            await _repository.DeleteAsync(ID);
            return "ssss";
        }

        public async Task<string> EditAsync(Section branch)
        { await _repository.UpdateAsync(branch); return ""; }

        public IQueryable<Section> FilterSectionPaginatedQuerable(string search)
        {
            var querable = _repository.GetTableNoTracking().AsQueryable();
            if (search != null)
            {
                querable = querable.Where(x => x.Name.Contains(search));
            }
            querable.OrderBy(x => x.DisplayOrder);
            return querable;

        }

        public async Task<List<Section>> GetAllSectionAsync()
    => _repository.GetTableNoTracking().ToList<Section>();

        public async Task<Section> GetSectionByIDAsync(int ID)
    => await _repository.GetByIdAsync(ID);

        public IQueryable<Section> GetSectionQuerable()
      => _repository.GetTableNoTracking().AsQueryable();

        public Task<bool> IsUniqueAsync(Expression<Func<Section, bool>> predicate, int? excludeId = null, CancellationToken cancellationToken = default)
      => _repository.IsUniqueAsync(predicate, excludeId, cancellationToken);
    }
}
