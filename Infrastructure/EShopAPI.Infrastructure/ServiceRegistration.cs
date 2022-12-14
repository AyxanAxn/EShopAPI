using EShopAPI.Appilication.Abstractions.Storage;
using EShopAPI.Appilication.Abstractions.Token;
using EShopAPI.Infrastructure.Enums;
using EShopAPI.Infrastructure.Services.Storage;
using EShopAPI.Infrastructure.Services.Storage.Azure;
using EShopAPI.Infrastructure.Services.Storage.Local;
using EShopAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace EShopAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection)
            where T : Storage, IStorage
        {

            serviceCollection.AddScoped<IStorage, T>();
            serviceCollection.AddScoped<ITokenHandler, TokenHandler>();
        }

        public static void AddStorage<T>(this IServiceCollection serviceCollection, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}