using EShopAPI.Appilication.IRepositories;
using P = EShopAPI.Domain.Entities;
using EShopAPI.Persistance.Contexts;

namespace EShopAPI.Persistance.Repositories
{
    internal class ProductImageFileReadRepository : ReadRepository<P::ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(EShopAPIDbContext context) : base(context)
        { }
    }
}
