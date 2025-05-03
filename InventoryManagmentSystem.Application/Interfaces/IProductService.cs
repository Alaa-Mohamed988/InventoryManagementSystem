namespace InventoryManagmentSystem.Service.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync();
        //Task<ProductDto?> GetProductByIdAsync(int id);
        //Task AddProductAsync(CreateProductDto productDto);
        Task UpdateProductAsync(int id, UpdateProductDto productDto);
        Task DeleteProductAsync(int id);
    }

}
