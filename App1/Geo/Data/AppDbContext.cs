using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Geo.Model;
using App1.Vlad.Model;

namespace App1.Geo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Delivery> Delivery { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.ToTable("delivery");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("delivery_id");
                
                // Configure the relationship with Order
                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Configure constraints
                entity.Property(e => e.Distance).HasAnnotation("CheckConstraint", "distance >= 0");
                entity.Property(e => e.TruckId).HasAnnotation("CheckConstraint", "truckid > 0");
                entity.Property(e => e.Weight).HasAnnotation("CheckConstraint", "cargo_weight >= 0");

                // Configure timestamps
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
