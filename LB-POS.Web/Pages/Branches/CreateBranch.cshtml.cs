using LB_POS.Core.Features.Brnach.Commands.Models; // تأكد من المسار الصحيح
using LB_POS.Data.DTOs;
using LB_POS.Service.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.Branches
{
    public class CreateBranchModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateBranchModel(IMediator mediator, LocationService locationService)
        {
            _mediator = mediator;
            _locationService = locationService;
        }

        [BindProperty]
        public AddBranchCommand Command { get; set; } = new();

        public List<Country> Countries { get; set; } = new();

        // في الـ Constructor
        private readonly LocationService _locationService;

        public async Task OnGetAsync()
        {
            Countries = await _locationService.GetLocationsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var response = await _mediator.Send(Command);

            if (response.Succeeded)
            {
                // يمكنك إضافة Toast Notification هنا
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return Page();
        }
    }
}