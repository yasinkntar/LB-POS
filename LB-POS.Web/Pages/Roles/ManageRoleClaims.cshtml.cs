using LB_POS.Core.Features.Authorization.Commands.Models;
using LB_POS.Core.Features.Authorization.Queries.Models;
using LB_POS.Data.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.Roles
{
    public class ManageRoleClaimsModel : PageModel
    {
        private readonly IMediator _mediator;

        public ManageRoleClaimsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public UpdateRoleClaimsCommand RoleClaimsManage { get; set; }

        public string RoleName { get; set; }

        // --- دالة الاستعلام (GET) ---
        public async Task<IActionResult> OnGetAsync(int roleId)
        {
            // نرسل استعلام للميدياتور لجلب البيانات المجهزة من السيرفس
            var response = await _mediator.Send(new GetManageRoleClaimsQuery(roleId));

            if (response.Succeeded)
            {
                RoleName = response.Data.RoleName;
                // تحويل البيانات القادمة من الـ Result إلى الـ Command المرتبط بالصفحة
                RoleClaimsManage = new UpdateRoleClaimsCommand
                {
                    RoleId = response.Data.RoleId,

                    RoleClaims = response.Data.RoleClaims.Select(x => new RoleClaims
                    {
                        DisplayName = x.DisplayName,
                        GroupName = x.GroupName,
                        Type = x.Type,
                        Value = x.Value
                    }).ToList()
                };
                return Page();
            }

            return RedirectToPage("/Error");
        }

        // --- دالة الحفظ (POST) ---
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // إرسال الـ Command مباشرة للميدياتور للتنفيذ في السيرفس
            var response = await _mediator.Send(RoleClaimsManage);

            if (response.Succeeded)
            {
                // إضافة رسالة نجاح (Toastr أو SweetAlert)
                TempData["Success"] = "تم تحديث صلاحيات الدور بنجاح";
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return Page();
        }
    }
}