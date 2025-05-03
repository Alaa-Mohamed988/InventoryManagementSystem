using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagmentSystem.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryManagmentSystem.Service.CQRS.Products.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductRepository _repo;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IProductRepository repo, ILogger<DeleteProductCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
             
            try
            {
                var product = await _repo.GetByIdAsync(request.Id);
                if (product == null) return false;

               await _repo.Delete(product);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    $"Failed to delete product {request.Id}", request.Id);
                return false;
            }
        }
    }
}
