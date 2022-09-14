using EShopAPI.Appilication.IRepositories;
using F = EShopAPI.Domain.Entities;
using EShopAPI.Persistance.Contexts;


namespace EShopAPI.Persistance.Repositories
{
    public class FileReadRepository : ReadRepository<F::File>, IFileReadRepository
    {
        public FileReadRepository(EShopAPIDbContext context) : base(context)
        { }
    }
}