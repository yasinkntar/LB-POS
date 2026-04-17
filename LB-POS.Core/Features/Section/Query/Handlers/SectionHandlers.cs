using AutoMapper;
using LB_POS.Core.Base;
using LB_POS.Core.Features.Section.Query.Models;
using LB_POS.Core.Features.Section.Query.Result;
using LB_POS.Core.Resources;
using LB_POS.Core.Wrappers;
using LB_POS.Service.IService;
using MediatR;
using Microsoft.Extensions.Localization;

namespace LB_POS.Core.Features.Section.Query.Handlers
{
    public class SectionHandlers : ResponseHandler,
                                   IRequestHandler<GetSectionPaginatedList, PaginatedResult<GetSectionListResponse>>
    {
        private readonly ISectionsService _sectionsService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public SectionHandlers(
            ISectionsService sectionsService,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _sectionsService = sectionsService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<PaginatedResult<GetSectionListResponse>> Handle(GetSectionPaginatedList request, CancellationToken cancellationToken)
        {
            // 1. جلب الاستعلام الأساسي من الـ Service
            var filterQuery = _sectionsService.FilterSectionPaginatedQuerable(request.Search);

            // 2. تنفيذ الترقيم وجلب البيانات من قاعدة البيانات كـ DTO لتجنب خطأ ترجمة الـ SQL
            var paginatedDtos = await filterQuery.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            // 3. تحويل الداتا (التي أصبحت في الذاكرة) باستخدام AutoMapper لتعمل دالة Localize بأمان
            var mappedData = _mapper.Map<List<GetSectionListResponse>>(paginatedDtos.Data);

            // 4. تجميع النتيجة النهائية في كائن PaginatedResult جديد
            var result = new PaginatedResult<GetSectionListResponse>(mappedData);

            return result;
        }
    }
}