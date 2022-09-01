using EShopAPI.Appilication.Repositories;
using EShopAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAPI.Appilication.IRepositories
{
    public interface IOrderWriteRepository : IWriteRepository<Order>
    {
    }
}
