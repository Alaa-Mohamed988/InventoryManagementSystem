namespace InventoryManagmentSystem.Domain.Entities
{
    public class Product : BaseEntity
    {

        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }
        public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();

    }
}
