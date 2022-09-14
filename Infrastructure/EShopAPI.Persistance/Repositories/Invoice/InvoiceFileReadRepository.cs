using EShopAPI.Appilication.IRepositories;
using EShopAPI.Persistance.Contexts;
using I = EShopAPI.Domain.Entities;

namespace EShopAPI.Persistance.Repositories
{
    public class InvoiceFileReadRepository : ReadRepository<I::InvoiceFile>, IInvoiceFileReadRepository
    {
        public InvoiceFileReadRepository(EShopAPIDbContext context) : base(context)
        { }
    }
}
