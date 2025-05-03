
namespace InventoryManagmentSystem.Service.DTOs
{
    public class TransferStockDto
    {
        public int SourceWarehouseId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public List<TransferItemDto> Items { get; set; } = new();
    }

}
