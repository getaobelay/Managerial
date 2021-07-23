// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class ProductCategory : AuditableEntity
    {

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Product> Products { get; set; }
    }

    internal class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.BaseEntityBuilder();

            builder.HasOne(p => p.Category)
                   .WithMany(p => p.ProductCategories)
                   .HasForeignKey(p => p.CategoryId);

            builder.ToTable($"product_categories");
        }
    }
}