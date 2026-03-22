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
                                                  IRequestHandler<GetStudentPaginatedListQuery, PaginatedResult<GetBranchListResponse>>
    {
        public readonly IBranchService BranchService;
        public readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public BranchHandler(IBranchService branchService, IMapper _mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            BranchService = branchService;
            mapper = _mapper;
            _stringLocalizer = stringLocalizer;
        }



        public async Task<Response<GetSingleBranchResponse>> Handle(GetBranchByIDQuery request, CancellationToken cancellationToken)
        {
            var result = await BranchService.GetBrancheByIDAsync(request.ID);
            if (result == null)
                return NotFound<GetSingleBranchResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var mappedResult = mapper.Map<GetSingleBranchResponse>(result);

            return Success(mappedResult);
        }

        public async Task<PaginatedResult<GetBranchListResponse>> Handle(GetStudentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var FilterQuery = BranchService.FilterBranchPaginatedQuerable(request.OrderBy, request.Search);
            var PaginatedList = await mapper.ProjectTo<GetBranchListResponse>(FilterQuery).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { Count = PaginatedList.Data.Count() };
            return PaginatedList;
        }
    }
}
