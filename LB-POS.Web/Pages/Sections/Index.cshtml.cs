using LB_POS.Core.Features.Section.Query.Models;
using LB_POS.Core.Features.Section.Query.Result;
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

        // تم تغيير الاسم إلى Sections (جمع) ليتطابق مع ملف الـ HTML الذي بنيناه سابقاً
        public PaginatedResult<GetSectionListResponse> Sections { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        // 👈 تمت إضافة BindProperty لكي يعمل كمبو تحديد عدد الأسطر بنجاح
        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        // 👈 الحل الجذري: تغيير void إلى Task، وإضافة Async لاسم الدالة
        public async Task OnGetAsync()
        {
            try
            {
                var query = new GetSectionPaginatedList
                {
                    Search = SearchTerm,
                    PageNumber = PageNumber,
                    PageSize = PageSize
                };

                // الآن السيرفر سينتظر بأدب حتى تنتهي هذه العملية ولن يغلق الاتصال
                var result = await _mediator.Send(query);

                if (result != null && result.Succeeded)
                {
                    Sections = result;
                }
                else
                {
                    TempData["Error"] = result?.Messages?.FirstOrDefault() ?? "فشل تحميل البيانات";

                    // 👈 تهيئة الكائن بقائمة فارغة لتجنب انفجار الصفحة (NullReferenceException) في الـ HTML
                    Sections = new PaginatedResult<GetSectionListResponse>(new List<GetSectionListResponse>())
                    {
                        TotalCount = 0,
                        TotalPages = 1,
                        CurrentPage = PageNumber,
                        PageSize = PageSize
                    };
                }
            }
            catch (Exception ex)
            {
                // في بيئة الإنتاج يفضل استخدام ILogger هنا بدلاً من رمي الخطأ
                TempData["Error"] = "حدث خطأ غير متوقع أثناء الاتصال بقاعدة البيانات.";
                throw;
            }
        }
    }
}