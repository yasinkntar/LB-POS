using LB_POS.Core.Features.Authorization.Commands.Models;
using LB_POS.Core.Features.Authorization.Queries.Models;
using LB_POS.Data.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.Roles
{
    public class ManageUserRolesModel : PageModel
    {
        private readonly IMediator _mediator;
        public ManageUserRolesModel(IMediator mediator) => _mediator = mediator;

        [BindProperty]
        public ManageUserRolesResult UserRolesManage { get; set; }

        [BindProperty]
        public string UserName { get; set; }

        public async Task<IActionResult> OnGetAsync(int userId)
        {
            var response = await _mediator.Send(new ManageUserRolesQuery() { UserId = userId });

            if (response.Succeeded)
            {
                UserRolesManage = response.Data;
                // نفترض أن الاستجابة تحتوي على اسم المستخدم للعرض فقط
                UserName = response.Data.FullName.ToString();
                return Page();
            }

            return RedirectToPage("/UserManger/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var command = new UpdateUserRolesCommand()
            {
                UserId = UserRolesManage.UserId,
                userRoles = UserRolesManage.userRoles.Select(x => new UserRoles // تحويل البيانات للـ Command
                {
                    Id = x.Id,
                    Name = x.Name,
                    HasRole = x.HasRole
                }).ToList()
            };

            var response = await _mediator.Send(command);

            if (response.Succeeded)
            {
                TempData["Success"] = "تم تحديث أدوار المستخدم بنجاح";
                return RedirectToPage("/UserManger/Index");
            }

            ModelState.AddModelError("", response.Message);
            return Page();
        }
    }
}