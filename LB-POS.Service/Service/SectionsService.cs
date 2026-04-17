using LB_POS.Data.DTOs.SectionDtos;
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
        {
            await _repository.UpdateAsync(branch);
            return "";
        }

        // 👈 تم تعديل نوع الإرجاع ليتوافق مع الـ DTO
        public IQueryable<SectionSummaryDto> FilterSectionPaginatedQuerable(string search)
        {
            // 1. استخدام Select لجلب البيانات المطلوبة فقط وتفادي مشاكل الـ Include
            var result = _repository.GetTableNoTracking()
                .Select(s => new SectionSummaryDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    NameEn = s.NameEn,
                    BranchName = s.Branch.Name,
                    BranchNameEn = s.Branch.NameEn,
                    TablesCount = s.Tables.Count(),
                    WaitersCount = s.Waiters.Count(),
                    // افتراض أن الخاصية DisplayOrder موجودة، وإلا يمكنك نقل الترتيب قبل الـ Select
                })
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                result = result.Where(x => x.Name.Contains(search));
            }

            // 2. تصحيح خطأ الـ OrderBy بإسناد النتيجة للمتغير result
            // (قم بإلغاء التعليق إذا كانت DisplayOrder موجودة في SectionSummaryDto)
            // result = result.OrderBy(x => x.DisplayOrder); 

            return result;
        }

        public async Task<List<Section>> GetAllSectionAsync()
            => _repository.GetTableNoTracking().ToList();

        public async Task<Section> GetSectionByIDAsync(int ID)
            => await _repository.GetByIdAsync(ID);

        public IQueryable<Section> GetSectionQuerable()
            => _repository.GetTableNoTracking().AsQueryable();

        public Task<bool> IsUniqueAsync(Expression<Func<Section, bool>> predicate, int? excludeId = null, CancellationToken cancellationToken = default)
            => _repository.IsUniqueAsync(predicate, excludeId, cancellationToken);
    }
}