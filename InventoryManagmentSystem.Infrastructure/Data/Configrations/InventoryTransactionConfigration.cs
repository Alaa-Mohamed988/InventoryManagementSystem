

namespace InventoryManagmentSystem.Repository.Data.Configrations
{
    public class InventoryTransactionConfigration : IEntityTypeConfiguration<InventoryTransaction>
    {
        public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
        {
            builder.HasOne(p => p.Product)
                 .WithMany()
            .HasForeignKey(p => p.ProductId);

            builder
        .HasOne(it => it.User)
        .WithMany() 
        .HasForeignKey(it => it.UserId);

        }
    }
}
