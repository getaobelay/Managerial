using Domain.Entites;
using Domain.Entites.Identity;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class ManagerialDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ManagerialDbContext(DbContextOptions<ManagerialDbContext> options)
                : base(options)
        {
        }

        public ManagerialDbContext()
        {
        }

        public string CurrentUserId { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WarehouseItem> WarehouseItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=(local);Database=Managerial;Trusted_Connection=True;MultipleActiveResultSets=true", b =>
                    b.MigrationsAssembly("Managerial"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}