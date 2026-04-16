using LB_POS.Core.Features.Authorization.Commands.Models;
using LB_POS.Core.Features.Authorization.Queries.Models;
using LB_POS.Core.Features.Authorization.Queries.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        public IndexModel(IMediator mediator) => _mediator = mediator;

        public List<GetRolesListResult> RolesList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var response = await _mediator.Send(new GetRolesListQuery());
            if (response.Succeeded)
            {
                RolesList = response.Data;
            }
            return Page();
        }

        // معالج إضافة دور جديد من الـ Modal
        public async Task<IActionResult> OnPostCreateRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) return Page();

            var response = await _mediator.Send(new AddRoleCommand { RoleName = roleName });

            if (response.Succeeded)
            {
                TempData["Success"] = "تم إضافة الدور بنجاح";
            }
            else
            {
                TempData["Error"] = response.Message;
            }

            return RedirectToPage();
        }
    }
}