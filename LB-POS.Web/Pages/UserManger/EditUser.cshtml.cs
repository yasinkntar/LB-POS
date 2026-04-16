using LB_POS.Core.Behavior;
using LB_POS.Core.Features.ApplicationUser.Commands.Models;
using LB_POS.Core.Features.ApplicationUser.Queries.Models;
using LB_POS.Data.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.UserManger
{
    [HasPermission(Permission.UserManger.EditUser)]
    public class EditUserModel : PageModel
    {
        private readonly IMediator _mediator;

        public EditUserModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public EditUserCommand Input { get; set; }

        // جلب البيانات عند فتح الصفحة
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // جلب البيانات من قاعدة البيانات باستخدام Mediator أو Service
            var response = await _mediator.Send(new GetUserByIdQuery(id));

            if (!response.Succeeded) return NotFound();
            var user = response.Data;
            if (user == null)
                return NotFound();
            // تعبئة الـ Command بالبيانات الحالية ليراها المستخدم في الحقول
            Input = new EditUserCommand
            {
                Id = user.ID,
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,

            };
            return Page();
        }

        // إرسال التعديلات
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // إرسال الأمر عبر MediatR
            var response = await _mediator.Send(Input);

            if (response.Succeeded)
            {
                TempData["Success"] = "تم تحديث بيانات المستخدم بنجاح";
                return RedirectToPage("Index");
            }
            else
            {
                // عرض الأخطاء القادمة من الـ Validation أو الـ Logic في الهاندلر
                ModelState.AddModelError(string.Empty, response.Message);
                return Page();
            }
        }
    }
}