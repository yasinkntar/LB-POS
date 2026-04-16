using LB_POS.Core.Features.Brnach.Commands.Models;
using LB_POS.Core.Features.Brnach.Queries.Models;
using LB_POS.Data.DTOs;
using LB_POS.Service.Service;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LB_POS.Web.Pages.Branches
{
    public class EditBranchModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly LocationService _locationService;

        public EditBranchModel(IMediator mediator, LocationService locationService)
        {
            _mediator = mediator;
            _locationService = locationService;
        }
        public List<Country> Countries { get; set; } = new();

        [BindProperty]
        public EditBranchCommand Command { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            //if (string.IsNullOrEmpty(id)) return NotFound();
            Countries = await _locationService.GetLocationsAsync();
            // جلب البيانات الحالية للفرع
            var response = await _mediator.Send(new GetBranchByIDQuery()
            {
                ID = id
            });

            if (!response.Succeeded) return RedirectToPage("Index");

            // تحويل البيانات من Query Result إلى Command (Manual Mapping)
            var data = response.Data;
            Command = new EditBranchCommand
            {
                Code = data.Code,
                Name = data.Name,
                NameEn = data.NameEn,
                ActivityCode = data.ActivityCode,
                SyndicateLicenseNumber = data.SyndicateLicenseNumber,
                Country = data.Address.Country,
                Governate = data.Address.Governate,
                GovernateEn = data.Address.GovernateEn,
                RegionCity = data.Address.RegionCity,
                RegionCityEn = data.Address.RegionCityEn,
                Street = data.Address.Street,
                StreetEn = data.Address.StreetEn,
                BuildingNumber = data.Address.BuildingNumber,
                BuildingNumberEn = data.Address.BuildingNumberEn,
                PostalCode = data.Address.PostalCode,
                Floor = data.Address.Floor,
                FloorEn = data.Address.FloorEn,
                Room = data.Address.Room,
                RoomEn = data.Address.RoomEn,
                Landmark = data.Address.Landmark,
                LandmarkEn = data.Address.LandmarkEn,

            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await _mediator.Send(Command);

            if (result.Succeeded)
            {
                // إرجاع رسالة نجاح عبر TempData إذا أردت
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, result.Message);
            return Page();
        }
    }
}