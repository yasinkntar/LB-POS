using LB_POS.Core.Features.ApplicationUser.Commands.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.UserManger
{
    public class CreateUserModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateUserModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public AddUserCommand AddUser { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _mediator.Send(AddUser);

            // حسب نوع الـ Response عندك
            if (result.Succeeded)
            {
                TempData["Success"] = "User created successfully";
                return RedirectToPage("/UserManger/Index");
            }

            TempData["Error"] = result.Message;
            return Page();
        }
    }
}