
namespace InventoryManagmentSystem.Application.DTOs
{
    public class CreateProductDto
    {
        public string UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }
        public int? WarehouseId { get; set; }
    }

}
