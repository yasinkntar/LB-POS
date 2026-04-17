using LB_POS.Core.Features.Brnach.Queries.Models;
using LB_POS.Core.Features.Section.Command.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LB_POS.Web.Pages.Sections
{
    public class CreateSectionModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateSectionModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        // الكائن الذي سيتم ربطه بحقول الشاشة
        [BindProperty]
        public AddSectionCommand Command { get; set; } = new AddSectionCommand();

        // قائمة الفروع لتعبئة الـ Dropdown
        public SelectList BranchesList { get; set; }

        public async Task OnGetAsync()
        {
            await LoadBranchesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadBranchesAsync(); // إعادة تحميل الفروع إذا كان هناك خطأ
                return Page();
            }

            try
            {
                // إرسال الأمر للـ Handler لإضافة القسم في الداتا بيز
                var result = await _mediator.Send(Command);

                if (result.Succeeded)
                {
                    TempData["Success"] = "تمت إضافة القسم بنجاح.";
                    return RedirectToPage("Index"); // العودة لجدول الأقسام
                }
                else
                {
                    TempData["Error"] = result.Message ?? "فشل في الإضافة";
                    await LoadBranchesAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "حدث خطأ غير متوقع أثناء الحفظ.";
                await LoadBranchesAsync();
                return Page();
            }
        }

        // دالة مساعدة لجلب الفروع من قاعدة البيانات وعرضها في الـ Dropdown
        private async Task LoadBranchesAsync()
        {

            var branchesResult = await _mediator.Send(new GetBranchListQuery());

            if (branchesResult != null && branchesResult.Succeeded)
            {
                BranchesList = new SelectList(branchesResult.Data, "Id", "Name");
            }
        }
    }
}
