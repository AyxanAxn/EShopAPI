using EShopAPI.Appilication.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace EShopAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            this._storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public Task DeleteAsync(string pathOrContainerName, string fileName)
            => _storage.DeleteAsync(pathOrContainerName, fileName);

        public List<string> GetFiles(string pathOrContainerName)
            => _storage.GetFiles(pathOrContainerName);

        public bool HasFile(string pathOrContainerName, string fileName)
            => _storage.HasFile(pathOrContainerName, fileName);
        public Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName,
            IFormFileCollection files)
           => _storage.UploadAsync(pathOrContainerName, files);
    }
}
