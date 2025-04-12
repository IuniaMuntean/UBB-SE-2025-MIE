using App1.Vlad.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Toma.Data
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
            modelBuilder.Entity<Order>().HasIndex(o => o.OrderId).IsUnique();
        }
    }
} 