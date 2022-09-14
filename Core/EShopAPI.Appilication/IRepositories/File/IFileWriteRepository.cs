using EShopAPI.Appilication.Repositories;
using F = EShopAPI.Domain.Entities;

namespace EShopAPI.Appilication.IRepositories
{
    public interface IFileWriteRepository : IWriteRepository<F::File>
    {}
}
