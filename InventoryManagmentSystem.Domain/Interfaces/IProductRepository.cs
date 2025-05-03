namespace InventoryManagmentSystem.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }

}
