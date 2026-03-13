using Microsoft.AspNetCore.Http;

namespace LB_POS.Service.IService
{
    public interface IFileService
    {
        public Task<string> UploadImage(string Location, IFormFile file);
    }
}
