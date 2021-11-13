using Application.ViewModels;
using Domain.Entites;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class AllocationConfig : IEntityTypeConfiguration<Allocation>
    {
        public void Configure(EntityTypeBuilder<Allocation> builder)
        {
            builder.BaseEntityBuilder();


            builder.HasOne(w => w.WarehouseItem)
                   .WithOne(w => w.Allocation)
                   .HasForeignKey<Allocation>(w=> w.Id);

            builder.HasOne(w => w.OrderDetail)
           .WithOne(w => w.Allocation)
           .HasForeignKey<Allocation>(w => w.Id);

        }
    }
}