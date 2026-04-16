using LB_POS.Data.DTOs;
using Microsoft.AspNetCore.Hosting;
using System.Text.Json;

namespace LB_POS.Service.Service
{
    public class LocationService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocationService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // تعديل نوع الإرجاع ليكون قائمة من الدول
        public async Task<List<Country>> GetLocationsAsync()
        {
            // تأكد أن اسم الملف يتطابق مع الملف الموجود في wwwroot
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Json/data.json");

            if (!File.Exists(filePath)) return new List<Country>();

            string jsonString = await File.ReadAllTextAsync(filePath);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // القراءة الآن تتم كـ List<Country> لأن الـ JSON يبدأ بـ [ { "id": 1, "name_ar": "مصر" ... } ]
            return JsonSerializer.Deserialize<List<Country>>(jsonString, options) ?? new List<Country>();
        }

        // ميثود إضافية اختيارية إذا كنت تريد جلب محافظات دولة معينة فقط بالـ ID
        public async Task<List<Governorate>> GetGovernoratesByCountryIdAsync(int countryId)
        {
            var countries = await GetLocationsAsync();
            var country = countries.FirstOrDefault(c => c.Id == countryId);
            return country?.Governorates ?? new List<Governorate>();
        }
    }
}
