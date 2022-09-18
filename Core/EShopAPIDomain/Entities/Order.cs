using EShopAPI.Domain.Entities.Common;

namespace EShopAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Adress { get; set; }
        public string Description { get; set; }
        public Guid CostumerId { get; set; }
        public Costumer Costumer { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}