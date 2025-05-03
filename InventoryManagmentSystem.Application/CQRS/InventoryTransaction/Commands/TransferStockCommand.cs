using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagmentSystem.Service.DTOs;
using InventoryManagmentSystem.Service.Interfaces;
using MediatR;

namespace InventoryManagmentSystem.Service.CQRS.InventoryTransaction.Commands
{
    public class TransferStockCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public int DestinationWarehouseId { get; set; }
        public List<TransferStockDto> Items { get; set; }
    }

  
    public class TransferStockCommandHandler : IRequestHandler<TransferStockCommand, bool>
    {
        private readonly IInventoryTransactionRepository _inventoryTransactionRepository;
        private readonly IMapper _mapper;

        public TransferStockCommandHandler(IInventoryTransactionRepository inventoryTransactionRepository,IMapper mapper)
        {
            _inventoryTransactionRepository = inventoryTransactionRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(TransferStockCommand request, CancellationToken cancellationToken)
        {
            List<ProductWarehouse> products = _mapper.Map<List<ProductWarehouse>>(request.Items);
            return await _inventoryTransactionRepository.TransferStockAsync(products,request.DestinationWarehouseId, request.UserId);
          
        }
    }



}
