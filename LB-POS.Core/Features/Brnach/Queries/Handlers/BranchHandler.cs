using AutoMapper;
using LB_POS.Core.Base;
using LB_POS.Core.Features.Brnach.Queries.Models;
using LB_POS.Core.Features.Brnach.Queries.Result;
using LB_POS.Core.Resources;
using LB_POS.Core.Wrappers;
using LB_POS.Service.IService;
using MediatR;
using Microsoft.Extensions.Localization;

namespace LB_POS.Core.Features.Brnach.Queries.Handlers
{
    public class BranchHandler : ResponseHandler,
                                 IRequestHandler<GetBranchByIDQuery, Response<GetSingleBranchResponse>>,
                                 IRequestHandler<GetBranchListQuery, Response<List<GetBranchListResponse>>>,
                                 IRequestHandler<GetBrnachPaginatedListQuery, PaginatedResult<GetBranchListResponse>>
    {
        // 1. تحويل الحقول إلى Private وتوحيد أسماء المتغيرات
        private readonly IBranchService _branchService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public BranchHandler(
            IBranchService branchService,
            IMapper mapper,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _branchService = branchService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        // دالة جلب فرع واحد حسب الـ ID
        public async Task<Response<GetSingleBranchResponse>> Handle(GetBranchByIDQuery request, CancellationToken cancellationToken)
        {
            var result = await _branchService.GetBrancheByIDAsync(request.ID);
            if (result == null)
                return NotFound<GetSingleBranchResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var mappedResult = _mapper.Map<GetSingleBranchResponse>(result);

            return Success(mappedResult);
        }

        // دالة جلب الفروع مع الترقيم (Pagination)
        public async Task<PaginatedResult<GetBranchListResponse>> Handle(GetBrnachPaginatedListQuery request, CancellationToken cancellationToken)
        {
            // 2. إصلاح مشكلة ProjectTo عن طريق الترقيم أولاً ثم التحويل
            var filterQuery = _branchService.FilterBranchPaginatedQuerable(request.OrderBy, request.Search);

            // جلب البيانات من الداتا بيز لتصبح في الذاكرة
            var paginatedDtos = await filterQuery.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            // التحويل بأمان تام
            var mappedData = _mapper.Map<List<GetBranchListResponse>>(paginatedDtos.Data);

            // تجميع النتيجة
            var result = PaginatedResult<GetBranchListResponse>.Success(
                mappedData,
                paginatedDtos.TotalCount,
                request.PageNumber,
                request.PageSize
            );

            result.Meta = new { Count = mappedData.Count };
            return result;
        }

        // دالة جلب كل الفروع بدون ترقيم (للقوائم المنسدلة Dropdowns)
        public async Task<Response<List<GetBranchListResponse>>> Handle(GetBranchListQuery request, CancellationToken cancellationToken)
        {
            var result = await _branchService.GetAllBranchesAsync();

            // 3. إصلاح خطأ الـ Compile Error (تحويل List إلى List)
            var mappedResult = _mapper.Map<List<GetBranchListResponse>>(result);

            // 4. إرجاع النتيجة مغلفة بـ Success
            return Success(mappedResult);
        }
    }
}