using LB_POS.Core.Features.Authentication.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace LB_POS.Web.Pages.Auth
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IMediator _mediator;
        public LoginModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        [BindProperty]
        public LoginInput Input { get; set; }

        public class LoginInput
        {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string Password { get; set; }

            public bool RememberMe { get; set; }
        }
        public IActionResult OnGet()
        {
            // إذا كان المستخدم مسجل دخول
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // تحويله مباشرة إلى الصفحة الرئيسية
                return RedirectToPage("/Index");
            }

            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _mediator.Send(new SignInWithCookieCommand
            {
                UserName = Input.UserName,
                Password = Input.Password
            });

            if (result.Succeeded)
                return RedirectToPage("/Index");

            ModelState.AddModelError("", result.Message);
            return Page();
        }

    }
}
