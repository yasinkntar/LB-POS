using LB_POS.Core.Features.Authorization.Commands.Models;
using LB_POS.Core.Features.Authorization.Queries.Models;
using LB_POS.Data.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace LB_POS.Web.Pages.Roles
{
    [Breadcrumb("تخصيص الصلاحيات", FromPage = typeof(Pages.UserManger.IndexModel))]
    public class ManageUserClaimsModel : PageModel
    {
        private readonly IMediator _mediator;
        public ManageUserClaimsModel(IMediator mediator) => _mediator = mediator;

        [BindProperty]
        public ManageUserClaimsResult UserClaimsManage { get; set; }

        public string UserName { get; set; }

        public async Task<IActionResult> OnGetAsync(int userId)
        {
            var response = await _mediator.Send(new ManageUserClaimsQuery() { UserId = userId });

            if (response.Succeeded)
            {
                UserClaimsManage = response.Data;
                UserName = response.Data.FullName.ToString(); // نفترض وجود اسم المستخدم في الـ DTO
                return Page();
            }

            return RedirectToPage("/UserManger/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var command = new UpdateUserClaimsCommand()
            {
                UserId = UserClaimsManage.UserId,
                userClaims = UserClaimsManage.userClaims.Where(x => x.IsInheritedFromRole == false).Select(x => new UserClaims // تحويل النوع ليتوافق مع الـ Command
                {

                    Type = x.Type,
                    Value = x.Value
                }).ToList()
            };

            var response = await _mediator.Send(command);

            if (response.Succeeded)
            {
                TempData["Success"] = "تم تحديث صلاحيات العمليات بنجاح";
                return RedirectToPage("/UserManger/Index");
            }

            ModelState.AddModelError("", response.Message);
            return Page();
        }
    }
}