using LB_POS.Service.IService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace LB_POS.Service.Service
{
    public class FileService : IFileService
    {
        #region Fileds
        private readonly IWebHostEnvironment _webHostEnvironment;
        #endregion
        #region Constructors
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion
        #region Handle Functions
        public async Task<string> UploadImage(string Location, IFormFile file)
        {
            // تنظيف المسار والتحقق
            if (string.IsNullOrWhiteSpace(Location) ||
                Location.Contains("..") ||
                Path.IsPathRooted(Location))
            {
                return "InvalidLocation";
            }

            // التحقق من امتداد الملف
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                return "InvalidFileType";
            }

            // التحقق من حجم الملف (مثلاً: 5 ميجابايت كحد أقصى)
            if (file.Length > 5 * 1024 * 1024)
            {
                return "FileTooLarge";
            }

            // إنشاء اسم ملف فريد لتجنب الاستبدال
            var fileName = $"{Guid.NewGuid()}{extension}";

            // تكوين المسار الكامل داخل wwwroot
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, Location);
            var fullPath = Path.Combine(folderPath, fileName);

            // التأكد أن المسار النهائي لا يزال داخل wwwroot
            if (!fullPath.StartsWith(_webHostEnvironment.WebRootPath))
            {
                return "InvalidLocation";
            }

            if (file.Length > 0)
            {
                try
                {
                    // إنشاء المجلد إذا لم يكن موجوداً
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // حفظ الملف
                    using (var filestream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                    }

                    // إرجاع المسار بالنسبة للويب
                    return $"/{Location}/{fileName}".Replace("\\", "/");
                }
                catch (Exception)
                {
                    return "FailedToUploadImage";
                }
            }
            else
            {
                return "NoImage";
            }
        }
        #endregion
    }
}