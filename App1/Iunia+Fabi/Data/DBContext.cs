using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Iunia_Fabi.Model;
using Microsoft.EntityFrameworkCore;

namespace App1.Iunia_Fabi.Data
{
    internal class RoadDBContext : DbContext
    {
        public DbSet<Road> Roads { get; set; }

        public RoadDBContext(DbContextOptions<RoadDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Road>().HasKey(o => new { o.start, o.end });
            modelBuilder.Entity<Road>().HasIndex(o => new { o.start, o.end });
        }
    }
}
