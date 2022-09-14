using EShopAPI.Appilication.IRepositories;
using EShopAPI.Persistance.Contexts;
using I = EShopAPI.Domain.Entities;

namespace EShopAPI.Persistance.Repositories
{
    public class InvoiceFileWriteRepository : WriteRepository<I::InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(EShopAPIDbContext context) : base(context)
        { }
    }
}
