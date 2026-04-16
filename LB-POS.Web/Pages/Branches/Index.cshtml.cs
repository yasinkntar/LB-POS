using LB_POS.Core.Features.Brnach.Commands.Models;
using LB_POS.Core.Features.Brnach.Queries.Models;
using LB_POS.Core.Features.Brnach.Queries.Result;
using LB_POS.Core.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.Branches
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public PaginatedResult<GetBranchListResponse> Branch { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                var query = new GetBrnachPaginatedListQuery
                {
                    Search = SearchTerm,
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };
                var result = await _mediator.Send(query);

                if (result != null && result.Succeeded)
                {
                    Branch = result;
                }
                else
                {
                    // في حال فشل الـ Succeeded ولكن لم يحدث Exception
                    TempData["Error"] = result?.Messages.FirstOrDefault() ?? "فشل تحميل البيانات";
                    //Users = new PaginatedResult<GetUserPaginationReponse>; // كائن فارغ لتجنب خطأ null في View
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteBranchCommand() { ID = id });

                if (result != null && result.Succeeded)
                {
                    TempData["Success"] = "تم حذف المستخدم بنجاح";
                }
                else
                {
                    TempData["Error"] = result?.Errors.FirstOrDefault() ?? "فشل الحذف";
                }
            }
            catch
            {
                TempData["Error"] = "حدث خطأ غير متوقع";
            }

            return RedirectToPage(); // 👈 مهم
        }
    }
}
