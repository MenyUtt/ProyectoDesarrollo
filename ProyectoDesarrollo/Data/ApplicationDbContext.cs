using Microsoft.EntityFrameworkCore;
using ProyectoDesarrollo.Models;

namespace ProyectoDesarrollo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customers> customers { get; set; }
        public DbSet<Contacts> contacts { get; set; }
        public DbSet<Orders> orders { get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Product_Categories> product_categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>()
               .HasOne(o => o.Customer)
               .WithMany()
               .HasForeignKey(o => o.CUSTOMER_ID);

            modelBuilder.Entity<Contacts>()
               .HasOne(o => o.Customer)
               .WithMany()
               .HasForeignKey(o => o.CUSTOMER_ID);

            modelBuilder.Entity<Products>()
              .HasOne(o => o.Categories)
              .WithMany()
              .HasForeignKey(o => o.CATEGORY_ID);
        }


    }
}
