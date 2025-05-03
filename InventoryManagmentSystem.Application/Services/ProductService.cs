
using InventoryManagmentSystem.Service.Interfaces;

namespace InventoryManagmentSystem.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        //public async Task<ProductDto?> GetProductByIdAsync(int id)
        //{
        //    var product = await _productRepository.GetByIdAsync(id);
        //    return _mapper.Map<ProductDto>(product);
        //}

        //public async Task AddProductAsync(CreateProductDto dto)
        //{
        //    var product = _mapper.Map<Product>(dto);
        //    await _productRepository.AddAsync(product);
        //}

        public async Task UpdateProductAsync(int id, UpdateProductDto dto)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            _mapper.Map(dto, product);
            _productRepository.Update(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            _productRepository.Delete(product);
        }

       
    }


}
