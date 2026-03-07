using AutoMapper;
using LB_POS.Core.Base;
using LB_POS.Core.Features.Brnach.Commands.Models;
using LB_POS.Core.Resources;
using LB_POS.Data.Entities;
using LB_POS.Service.IService;
using MediatR;
using Microsoft.Extensions.Localization;

namespace LB_POS.Core.Features.Brnach.Commands.Handlers
{
    public class BranchCommandHandlers : ResponseHandler,
                                         IRequestHandler<AddBranchCommand, Response<string>>,
                                         IRequestHandler<EditBranchCommand, Response<string>>,
                                         IRequestHandler<DeleteBranchCommand, Response<string>>
    {
        public readonly IBranchService BranchService;
        public readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public BranchCommandHandlers(IBranchService branchService, IMapper _mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            BranchService = branchService;
            mapper = _mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddBranchCommand request, CancellationToken cancellationToken)
        {
            var branchMapping = mapper.Map<Branch>(request);
            var result = await BranchService.AddAsync(branchMapping);
            if (result != "Success")
                return UnprocessableEntity<string>(result);
            else if (result == "Success") return Created(result);
            else return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(EditBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await BranchService.GetBrancheByIDAsync(request.Id);
            if (branch == null)
                return NotFound<string>();
            var branchMapping = mapper.Map(request, branch);
            var result = await BranchService.EditAsync(branchMapping);
            //return response
            if (result == "Success") return Success("Success ");
            else return BadRequest<string>();
        }

        public async Task<Response<string>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await BranchService.GetBrancheByIDAsync(request.ID);
            //return NotFound
            if (branch == null) return NotFound<string>();
            //Call service that make Delete
            var result = await BranchService.DeleteAsync(branch);
            if (result == "Success") return Deleted<string>();
            else return BadRequest<string>();
        }
    }
}
