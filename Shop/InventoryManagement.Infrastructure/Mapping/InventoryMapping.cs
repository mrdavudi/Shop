﻿using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Inventory");

            builder.OwnsMany(x => x.Operations, modelBuilder =>
            {
                modelBuilder.HasKey(x => x.Id);
                modelBuilder.ToTable("InventoryOperation");
                modelBuilder.Property(x => x.Description).HasMaxLength(1000);
                modelBuilder.WithOwner(x => x.Inventory).HasForeignKey(x => x.InventoryId);
            });
        }
    }
}
