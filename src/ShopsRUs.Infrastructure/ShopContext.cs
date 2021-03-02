using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ShopsRus.Core;
using ShopsRus.Core.Entities;
using ShopsRUs.Infrastructure;

namespace ShopsRus.Infrastructure
{
    public class ShopContext: DbContext
    {        

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = ConfigurationLoader.LoadConfiguration();
            var sqliteFileName = config.GetValue<string>("SqliteFileName");
            optionsBuilder.UseSqlite($"Filename={sqliteFileName}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Customer>()
                .Property(e => e.CustomerType)
                .HasConversion(
                    v => v.ToString(),
                    v => (CustomerType)Enum.Parse(typeof(CustomerType), v));
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Discount> Discounts { get; set; }
    }
}
