using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagmentSystem.Domain.Entities
{
    public class Warehouse : BaseEntity
    {
        public string? Name { get; set; }
        public string? Location { get; set; } 
        public ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();
    }

}
