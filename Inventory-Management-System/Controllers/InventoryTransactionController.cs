
namespace Inventory_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public InventoryTransactionController(IMapper mapper,IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("transfer")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> TransferStock([FromBody] List<TransferStockDto> transferStockDto,int warehouseId,string UserId)
        {
            

            var result = await _mediator.Send(new TransferStockCommand { Items=transferStockDto ,DestinationWarehouseId = warehouseId,UserId=UserId });

            if (result)
            {
                return Ok(new { message = "Stock transferred successfully." });
            }

            return BadRequest(new { message = "Stock transfer failed." });
        }
    }
}
