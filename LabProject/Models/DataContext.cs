using LabProject.Resources.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabProject.Models
{
    public class DataContext: DbContext
    {
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<WearProduct> WearProducts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<UseWay> UseWays { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shoe>()
                .HasOne(p => p.Brand)
                .WithMany(t => t.Shoes)
                .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<Shoe>()
                .HasOne(p => p.UseWay)
                .WithMany(t => t.Shoes)
                .HasForeignKey(p => p.UseWayId);

            modelBuilder.Entity<WearProduct>()
                .HasOne(p => p.Brand)
                .WithMany(t => t.WearProducts)
                .HasForeignKey(p => p.BrandId);

            modelBuilder.Entity<WearProduct>()
                .HasOne(p => p.UseWay)
                .WithMany(t => t.WearProducts)
                .HasForeignKey(p => p.UseWayId);
        }
    }
}
