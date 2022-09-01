using EShopAPI.Appilication.IRepositories;
using EShopAPI.Domain.Entities;
using EShopAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAPI.Persistance.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>,IProductWriteRepository
    {
        public ProductWriteRepository(EShopAPIDbContext context) : base(context)
        {
        }
    }
}