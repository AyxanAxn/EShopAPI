using EShopAPI.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAPI.Persistance
{
    internal class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<EShopAPIDbContext>
    {
        public EShopAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<EShopAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}