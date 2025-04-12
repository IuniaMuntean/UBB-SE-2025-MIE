using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Iunia_Fabi.Model;
using App1.Vlad.Model;
using Microsoft.EntityFrameworkCore;

namespace App1.Iunia_Fabi.Data
{
    internal class CityDBContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        
        public CityDBContext(DbContextOptions<CityDBContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasKey(o => o.id);
            modelBuilder.Entity<City>().HasIndex(o => o.id).IsUnique();
        }
    }
}
