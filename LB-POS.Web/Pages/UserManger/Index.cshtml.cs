using LB_POS.Core.Features.ApplicationUser.Queries.Models;
using LB_POS.Core.Features.ApplicationUser.Queries.Result;
using LB_POS.Core.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.UserManger
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public PaginatedResult<GetUserPaginationReponse> Users { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public async Task OnGetAsync()
        {
            var query = new GetUserPaginationQuery
            {
                PageNumber = PageNumber,
                PageSize = PageSize
            };

            Users = await _mediator.Send(query);
        }
    }
}

