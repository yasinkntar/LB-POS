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
        public readonly ISectionsService SectionsService;
        public readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;

        public SectionHandlers(ISectionsService sectionsService, IMapper _mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            SectionsService = sectionsService;
            mapper = _mapper;
            _stringLocalizer = stringLocalizer;
        }



        public async Task<PaginatedResult<GetSectionListResponse>> Handle(GetSectionPaginatedList request, CancellationToken cancellationToken)
        {
            var FilterQuery = SectionsService.FilterSectionPaginatedQuerable(request.Search);
            var PaginatedList = await mapper.ProjectTo<GetSectionListResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;
        }
    }
}
