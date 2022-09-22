using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EShopAPI.Appilication
{
    public static class ServiceRegistration
    {
        public  static void AddAppilicationService(this IServiceCollection collection) {
            collection.AddMediatR(typeof(ServiceRegistration));        
        }
    }
}
