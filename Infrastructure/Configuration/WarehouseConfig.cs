using Domain.Entites;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class WarehouseConfig : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.BaseEntityBuilder();

            builder.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(true)
                .ValueGeneratedOnAdd()
                .IsRequired();
        }
    }
}