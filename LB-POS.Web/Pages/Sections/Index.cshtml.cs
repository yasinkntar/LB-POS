using LB_POS.Core.Features.Section.Query.Models;
using LB_POS.Core.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.Sections
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public PaginatedResult<GetSectionPaginatedList> Users { get; set; }
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }
        public void OnGet()
        {
        }
    }
}
