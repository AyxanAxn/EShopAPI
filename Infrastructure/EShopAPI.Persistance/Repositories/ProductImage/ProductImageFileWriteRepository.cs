using EShopAPI.Appilication.IRepositories;
using P = EShopAPI.Domain.Entities;
using EShopAPI.Persistance.Contexts;

namespace EShopAPI.Persistance.Repositories
{
    public class ProductImageFileWriteRepository:WriteRepository<P::ProductImageFile>,IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(EShopAPIDbContext context) : base(context)
        { }
    }
}
