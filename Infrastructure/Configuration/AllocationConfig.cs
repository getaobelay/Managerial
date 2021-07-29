using Domain.Entites;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class AllocationConfig : IEntityTypeConfiguration<Allocation>
    {
        public void Configure(EntityTypeBuilder<Allocation> builder)
        {
            builder.BaseEntityBuilder();

            builder.HasOne(o => o.Order)
              .WithMany(w => w.Allocations)
              .HasForeignKey(o => o.OrderID);

            builder.HasMany(d => d.WarehouseItems)
                 .WithOne(p => p.Allocation)
                 .HasForeignKey(d => d.AllocationId);
        }
    }
}