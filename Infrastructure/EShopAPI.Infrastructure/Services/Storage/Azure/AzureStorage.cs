using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EShopAPI.Appilication.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace EShopAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage :Storage, IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private BlobContainerClient _blobContainerClient;
        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }
        public async Task DeleteAsync(string ContainerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }
        public List<string> GetFiles(string ContainerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            return _blobContainerClient.GetBlobs().Select(x => x.Name).ToList();
        }
        public bool HasFile(string ContainerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(ContainerName);
            return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
        }
        public async Task<List<(string fileName, string pathOrContainerName)>> 
            UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> datas = new();
            foreach (var file in files)
            {

                string fileNewName=await FileRenameAsync(containerName, file.Name,HasFile);
                BlobClient blobClient = _blobContainerClient.GetBlobClient(fileNewName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((fileNewName, $"{containerName}/{fileNewName}"));
            }
            return datas;
        }
    }
}