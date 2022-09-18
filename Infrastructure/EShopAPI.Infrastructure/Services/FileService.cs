using EShopAPI.Infrastructure.Operation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EShopAPI.Infrastructure.Services
{
    public class FileService
    {
        readonly IWebHostEnvironment _webHostEnvironment;
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
    }
}