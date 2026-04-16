using Microsoft.AspNetCore.Razor.TagHelpers;
namespace LB_POS.Web.TagHelpers
{

    [HtmlTargetElement("*", Attributes = "asp-permission")]
    public class PermissionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionTagHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HtmlAttributeName("asp-permission")]
        public string Permission { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            // ❌ مش مسجل دخول
            if (user == null || !user.Identity.IsAuthenticated)
            {
                output.SuppressOutput();
                return;
            }

            // 👑 SuperAdmin يشوف كل شيء
            if (user.IsInRole("SuperAdmin"))
                return;

            // ✅ تحقق من الصلاحية
            var hasPermission = user.Claims.Any(c =>
                c.Type == "Permission" && c.Value == Permission);

            if (!hasPermission)
            {
                output.SuppressOutput();
            }
        }
    }
}
