using AutoMapper;
using LB_POS.Core.Base;
using LB_POS.Core.Features.Section.Command.Models;
using LB_POS.Core.Resources;
using LB_POS.Service.IService;
using MediatR;
using Microsoft.Extensions.Localization;

namespace LB_POS.Core.Features.Section.Command.Handlers
{
    public class SectionCommandHandlers : ResponseHandler,
                                        IRequestHandler<AddSectionCommand, Response<string>>
    {
        public readonly ISectionsService _sectionsService;
        public readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        public SectionCommandHandlers(ISectionsService sectionsService, IMapper _mapper, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _sectionsService = sectionsService;
            mapper = _mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<Response<string>> Handle(AddSectionCommand request, CancellationToken cancellationToken)
        {
            var sectionMapping = mapper.Map<Data.Entities.Section>(request);
            var result = await _sectionsService.AddAsync(sectionMapping);
            if (result != "Success")
                return UnprocessableEntity<string>(result);
            else if (result == "Success") return Created(result);
            else return BadRequest<string>(result);
        }
    }
}
