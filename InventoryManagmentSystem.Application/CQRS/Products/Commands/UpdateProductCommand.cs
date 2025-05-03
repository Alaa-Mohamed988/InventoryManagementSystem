using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryManagmentSystem.Service.CQRS.Products.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int LowStockThreshold { get; set; }
        public int? WarehouseId { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(
            IProductRepository repo,
            IMapper mapper,
            ILogger<UpdateProductCommandHandler> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _repo.GetByIdAsync(request.Id);
                if (product == null) return false;

                // map the product with the new values in request
                _mapper.Map(request, product);

                await _repo.Update(product);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Failed to update {request.Name} product",
                    request.Id, request);
                return false;
            }
        }
    }
}
