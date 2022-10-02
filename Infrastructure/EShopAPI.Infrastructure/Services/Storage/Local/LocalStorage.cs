using EShopAPI.Appilication.Abstractions.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace EShopAPI.Infrastructure.Services.Storage.Local
{
    public class LocalStorage : Storage, ILocalStorage
    {

        readonly IWebHostEnvironment _webHostEnvironment;
        public LocalStorage(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task DeleteAsync(string path, string fileName)
            => File.Delete($"{path}\\{fileName}");


        public List<string> GetFiles(string path)
        {
            DirectoryInfo directory = new(path);
            return directory.GetFiles().Select(x => x.Name).ToList();
        }

        public bool HasFile(string path, string fileName)
            => File.Exists($"{path}\\{fileName}");

        async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                return true;
            }
            catch (Exception ex)
            {
                //todo log!
                throw ex;
            }
        }

        public async Task<List<(string fileName, string pathOrContainerName)>>
            UploadAsync(string pathOrContainerName, IFormFileCollection files)
        {
            string uploadPath = Path.Combine
                (_webHostEnvironment.WebRootPath, pathOrContainerName);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            List<(string fileName, string path)> datas = new();

            foreach (IFormFile file in files)
            {
                string fileNewName =
                    await FileRenameAsync(pathOrContainerName, file.Name, HasFile);

                await CopyFileAsync($"{uploadPath}\\{fileNewName}", file);

                datas.Add((file.FileName, $"{uploadPath}\\{fileNewName}"));
            }
            return datas;
        }
    }
}