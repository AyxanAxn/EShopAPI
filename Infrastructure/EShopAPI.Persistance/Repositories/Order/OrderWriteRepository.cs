using EShopAPI.Appilication.IRepositories;
using EShopAPI.Appilication.Repositories;
using EShopAPI.Domain.Entities;
using EShopAPI.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAPI.Persistance.Repositories
{
    public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(EShopAPIDbContext context) : base(context)
        {
        }
    }
}
