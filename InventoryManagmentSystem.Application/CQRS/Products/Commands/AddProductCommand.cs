

namespace InventoryManagmentSystem.Service.CQRS.Products.Commands
{
    public class AddProductCommand:IRequest<bool>
    {
        public string UserId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }
        public int? WarehouseId { get; set; }
    }
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand,bool>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddProductCommandHandler> _logger;

        public AddProductCommandHandler(IProductRepository repository,IMapper mapper,ILogger<AddProductCommandHandler> logger)
        {
            _repository = repository;
           _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            try {
               await _repository.AddAsync(product);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error adding product",nameof(AddProductCommandHandler),request);

                return false;
            }
            
        }
    }
}
