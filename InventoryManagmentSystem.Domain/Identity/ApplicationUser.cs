

namespace InventoryManagmentSystem.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public List<InventoryTransaction> InventoryTransactions { get; set; }
    }
}
