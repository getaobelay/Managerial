using Domain.Common;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Extensions
{
    public static class EntityBuilderHelper
    {
        public static EntityTypeBuilder BaseStockBuilder<TStock>(this EntityTypeBuilder<TStock> builder)
            where TStock : StockEntity, new()
        {
            builder.BaseEntityBuilder();

            builder.Property(e => e.ProductQuantity)
                   .HasColumnType("decimal(8,2)")
                   .IsRequired();

            builder.Property(e => e.BatchQuantity)
                   .HasColumnType("decimal(8,2)")
                   .IsRequired();

            builder.Property(e => e.TotalUnitsQuantity)
                   .HasColumnType("decimal(8,2)")
                   .IsRequired();

            return builder;
        }

        public static EntityTypeBuilder BaseEntityBuilder<TBaseEntity>(this EntityTypeBuilder<TBaseEntity> builder)
            where TBaseEntity : AuditableEntity, new()
        {
            builder.Property(e => e.CreatedBy)
                   .HasMaxLength(50)
                   .IsUnicode(true)
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.UpdatedBy)
                  .HasMaxLength(50)
                  .IsUnicode(true);

            return builder;
        }
    }
}