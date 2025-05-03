

namespace InventoryManagmentSystem.Domain.Entities
{
    public class InventoryTransaction : BaseEntity
    {
        
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public Product? Product { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
