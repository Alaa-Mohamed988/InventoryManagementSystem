
namespace InventoryManagmentSystem.Service.Interfaces
{
    public interface IInventoryTransactionRepository
    {
        Task<bool> AddStockAsync(Product product, int warehouseId, int quantity, string userId);
        Task<bool> RemoveStockAsync(int productId, int warehouseId, int quantity, string userId);

        Task<bool> TransferStockAsync(List<ProductWarehouse> ProductsToTransfer,int DestenationWarehouseId,string UserId);
    }

}
