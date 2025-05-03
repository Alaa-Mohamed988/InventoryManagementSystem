using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Repository.Repositories
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddStockAsync(Product product, int warehouseId, int quantity, string userId)
        {
            var productWarehouse = await _context.ProductWarehouses
                .FirstOrDefaultAsync(pw => pw.ProductId == product.Id && pw.WarehouseId == warehouseId);

            if (productWarehouse == null)
            {
                productWarehouse = new ProductWarehouse
                {
                    ProductId = product.Id,
                    WarehouseId = warehouseId,
                    Quantity = quantity
                };
                _context.ProductWarehouses.Add(productWarehouse);
            }
            else
            {
                productWarehouse.Quantity += quantity;
                _context.ProductWarehouses.Update(productWarehouse);
            }

            var transaction = new InventoryTransaction
            {
                ProductId = product.Id,
                Quantity = quantity,
                TransactionType = TransactionType.AddStock,
                Date = DateTime.UtcNow,
                UserId = userId,
              
            };

            _context.InventoryTransactions.Add(transaction);
            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> RemoveStockAsync(int productId, int warehouseId, int quantity, string userId)
        {
            var productWarehouse = await _context.ProductWarehouses
                .FirstOrDefaultAsync(pw => pw.ProductId == productId && pw.WarehouseId == warehouseId);

            if (productWarehouse == null || productWarehouse.Quantity < quantity)
                return false;

            productWarehouse.Quantity -= quantity;

            InventoryTransaction inventoryTransaction = new InventoryTransaction()
            {
                TransactionType = TransactionType.RemoveStock,
                Date = DateTime.Now,
                Quantity = quantity,
                ProductId = productId,
                WarehouseId = warehouseId
            };

            _context.InventoryTransactions.Add(inventoryTransaction);
            await _context.SaveChangesAsync() ;
            return true;
        }

        public async Task<bool> TransferStockAsync(List<ProductWarehouse> productsToTransfer, int DestinationWarehouseId,string UserId)
        {
            foreach (var item in productsToTransfer)
            {
                
                var source = await _context.ProductWarehouses
                    .FirstOrDefaultAsync(pw => pw.ProductId == item.ProductId && pw.WarehouseId == item.WarehouseId);

                if (source == null || source.Quantity < item.Quantity)
                    return false; 

                
                source.Quantity -= item.Quantity;

                
                var destination = await _context.ProductWarehouses
                    .FirstOrDefaultAsync(pw => pw.ProductId == item.ProductId && pw.WarehouseId == DestinationWarehouseId);

                if (destination == null)
                {
                    destination = new ProductWarehouse
                    {
                        ProductId = item.ProductId,
                        WarehouseId = DestinationWarehouseId,
                        Quantity = item.Quantity
                    };
                    await _context.ProductWarehouses.AddAsync(destination);
                }
                else
                {
                    destination.Quantity += item.Quantity;
                    
                }

                
                var transaction = new InventoryTransaction
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    TransactionType = TransactionType.TransferStock,
                    Date = DateTime.UtcNow,
                    UserId = UserId
                };
                await _context.InventoryTransactions.AddAsync(transaction);
            }

             await _context.SaveChangesAsync();
            return true;
        }


    }
}