using Domain.Entites;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class StockConfig : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.BaseStockBuilder();

            builder.HasMany(d => d.Batches)
                .WithOne(p => p.Stock)
                .HasForeignKey(d => d.StockId);

            builder.HasMany(d => d.Products)
                .WithOne(p => p.Stock)
                .HasForeignKey(d => d.StockId);
        }
    }
}