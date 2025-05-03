using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagmentSystem.Service.CQRS.InventoryTransaction.Commands;
using InventoryManagmentSystem.Service.CQRS.Products.Commands;
using InventoryManagmentSystem.Service.DTOs;

namespace InventoryManagmentSystem.Service.Profiles
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            // 1) DTO to Command
            CreateMap<CreateProductDto, AddProductCommand>();

            // 2) Command to product
            CreateMap<AddProductCommand, Product>();
                
            // 3) Product to DTO and vice versa
            CreateMap<Product, ProductDto>().ReverseMap();

            // 4) Product to UpdateProductDto in handler
            CreateMap<UpdateProductCommand,Product>();

            // 5) UpdateProductDto to ProductCommand for controller
            CreateMap<UpdateProductDto,UpdateProductCommand>();

            // 6) TransferStockDto to TransferStockCommand
            CreateMap<TransferStockDto,TransferStockCommand>();

            // 7)
            CreateMap<TransferStockDto, ProductWarehouse>();
        }
    }
}
