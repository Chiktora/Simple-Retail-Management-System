using Microsoft.EntityFrameworkCore;
using Simple_Retail_Management_System.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Simple_Retail_Management_System.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext()
        {
            
        }

        public ShopContext(DbContextOptions options):base(options) 
        {
            
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }

            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Configure precision for decimal properties
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2); // Sets the precision to 18 digits with 2 decimal places

            modelBuilder.Entity<Sale>()
                .Property(s => s.SalesPrice)
                .HasPrecision(18, 2); // Sets the precision to 18 digits with 2 decimal places

            // Set up relationships and navigation properties
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.Sales)
                .HasForeignKey(s => s.EmployeeId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId)
                .IsRequired(false); ;

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Sale)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(od => od.SaleId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Producer)
                .WithMany(pr => pr.Products)
                .HasForeignKey(p => p.ProducerId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

             modelBuilder.Entity<OrderDetail>().HasKey(od => new { od.SaleId, od.ProductId });

            modelBuilder.Entity<Product>().HasIndex(p => p.Barcode).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
