using EShopAPI.Domain.Entities;
using EShopAPI.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;


namespace EShopAPI.Persistance.Contexts
{
    public class EShopAPIDbContext : DbContext
    {
        public EShopAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Costumer> Costumers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker. it can see the new created and updated datas!
            var datas = ChangeTracker
                .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added=>data.Entity.CreatedDate=DateTime.UtcNow,
                    EntityState.Modified =>data.Entity.UpdatedDate=DateTime.UtcNow,
                    _=>DateTime.UtcNow,
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}