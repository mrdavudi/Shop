﻿using InventoryManagement.Domain.InventoryAgg;
using InventoryManagement.Infrastructure.Mapping;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure
{
    public class InventoryContext : DbContext
    {
        public DbSet<Inventory> Inventory { get; set; }
        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(InventoryMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
