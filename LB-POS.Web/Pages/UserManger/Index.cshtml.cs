using LB_POS.Core.Behavior;
using LB_POS.Core.Features.ApplicationUser.Commands.Models;
using LB_POS.Core.Features.ApplicationUser.Queries.Models;
using LB_POS.Core.Features.ApplicationUser.Queries.Result;
using LB_POS.Core.Wrappers;
using LB_POS.Data.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartBreadcrumbs.Attributes;

namespace LB_POS.Web.Pages.UserManger
{
    [Breadcrumb("المستخدمين", IconClasses = "fas fa-users")]
    [HasPermission(Permission.UserManger.ViewUsers)]
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
        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }


        public async Task OnGetAsync()
        {
            try
            {
                var query = new GetUserPaginationQuery
                {
                    Serarch = SearchTerm,
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };

                var result = await _mediator.Send(query);

                if (result != null && result.Succeeded)
                {
                    Users = result;
                }
                else
                {
                    // في حال فشل الـ Succeeded ولكن لم يحدث Exception
                    TempData["Error"] = result?.Messages.FirstOrDefault() ?? "فشل تحميل البيانات";
                    //Users = new PaginatedResult<GetUserPaginationReponse>; // كائن فارغ لتجنب خطأ null في View
                }
            }
            catch (Exception ex)
            {
                // هنا يمكنك رؤية الخطأ الحقيقي في الـ Debugger
                TempData["Error"] = "عذراً، حدث خطأ غير متوقع في النظام.";
                //Users = new PaginatedResult<GetUserPaginationReponse>();
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            try
            {
                var result = await _mediator.Send(new DeleteUserCommand(id));

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

