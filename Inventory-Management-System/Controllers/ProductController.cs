
using AutoMapper;
using InventoryManagmentSystem.Domain.Entities;
using InventoryManagmentSystem.Service.CQRS.Products.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Inventory_Management_System.Controllers
{
    [ApiController]                          
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Viewer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return products;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Manager,Viewer", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ProductDto> GetProductById(int id)
        {
            // mediator will search for the handler that implements
            // the GetProductByIdQuery and send it to the handler

            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
            return product;
        }


        [HttpPost]
        [Authorize(Roles = "Admin",AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ResponseViewModel<bool>> AddProduct([FromBody]CreateProductDto productDto)
        {
            AddProductCommand product = _mapper.Map<AddProductCommand>(productDto);
            bool success= await _mediator.Send(product);
            if (success)
            {
               
                return  ResponseViewModel<bool>.Success(success, "Product added successfully");
            }
            else
            {
                return ResponseViewModel<bool>.Error(ErrorCode.UnExcepectedError, "Product not added");
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseViewModel<bool>> Update(int id, [FromBody] UpdateProductDto updateDto)
        {
            UpdateProductCommand updateProduct = _mapper.Map<UpdateProductCommand>(updateDto);
            updateProduct.Id = id;
            var success = await _mediator.Send(updateProduct);
            if (!success)
                return ResponseViewModel<bool>
                    .Error(ErrorCode.NotFound, "Product not found or update failed");

            return ResponseViewModel<bool>.Success(true, "Product updated");
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteProductCommand { Id = id });
            if (!success)
                return ResponseViewModel<bool>
                    .Error(ErrorCode.NotFound, "Product not found or delete failed");

            return ResponseViewModel<bool>.Success(true, "Product deleted");
        }
    }

    
}
