using EShopAPI.Appilication.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using F= EShopAPI.Domain.Entities;

namespace EShopAPI.Appilication.IRepositories
{
    public interface IFileReadRepository : IReadRepository<F::File>
    {}
}
