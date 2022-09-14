using EShopAPI.Appilication.IRepositories;
using EShopAPI.Persistance.Contexts;
using F = EShopAPI.Domain.Entities;

namespace EShopAPI.Persistance.Repositories
{
    public class FileWriteRepository : WriteRepository<F::File>,
        IFileWriteRepository
    {
        public FileWriteRepository(EShopAPIDbContext context) : base(context)
        { }
    }
}
