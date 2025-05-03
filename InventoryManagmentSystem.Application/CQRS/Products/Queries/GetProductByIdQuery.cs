

using MediatR;

namespace InventoryManagmentSystem.Service.CQRS.Products.Queries
{
    // any class implement IRequest means
    // this class hold data which mediatR needed to process the request
    public class GetProductByIdQuery :IRequest<ProductDto>
    {
        public int Id { get; set; }
    }

    // any class implement IRequestHandler means
    // this class hold codes which mediatR exactly implement it based on the data
    // which is passed from the request class
    public class GetProductByIdQueryHandeler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandeler(IProductRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
           Product product =await _repository.GetByIdAsync(request.Id);
           return _mapper.Map<ProductDto>(product);
        }
    }
}
