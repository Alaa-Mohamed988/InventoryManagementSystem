using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Service.DTOs
{
    public class TransferItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
