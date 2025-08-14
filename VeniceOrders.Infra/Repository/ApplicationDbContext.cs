using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeniceOrders.Domain.Entities;

namespace VeniceOrders.Infra.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.ClienteId)
                      .IsRequired();

                entity.Property(o => o.Data)
                      .IsRequired();

                entity.Property(o => o.Status)
                      .IsRequired();
               
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Product)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(i => i.Price)
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();

                entity.Property(i => i.Quantity)
                      .IsRequired();
            });
        }
    }
}
