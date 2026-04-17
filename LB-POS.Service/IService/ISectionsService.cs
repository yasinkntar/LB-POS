using LB_POS.Data.DTOs.SectionDtos;
using LB_POS.Data.Entities;
using System.Linq.Expressions;

namespace LB_POS.Service.IService
{
    public interface ISectionsService
    {
        Task<List<Section>> GetAllSectionAsync();
        Task<Section> GetSectionByIDAsync(int ID);
        Task<string> AddAsync(Section branch);
        Task<string> EditAsync(Section branch);
        Task<string> DeleteAsync(Section ID);
        Task<bool> IsUniqueAsync(Expression<Func<Section, bool>> predicate, int? excludeId = null, CancellationToken cancellationToken = default);
        public IQueryable<Section> GetSectionQuerable();
        public IQueryable<SectionSummaryDto> FilterSectionPaginatedQuerable(string search);
    }
}
