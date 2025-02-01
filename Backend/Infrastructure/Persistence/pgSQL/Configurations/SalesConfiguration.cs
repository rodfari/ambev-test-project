using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace pgSQL.Configurations;

public class SalesConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
                builder.ToTable("Sales");

        builder.HasKey(s => s.Id);

        // builder.Property(s => s.SaleNumber)
        //     .IsRequired()
        //     .HasMaxLength(50);

        builder.Property(s => s.CustomerId)
            .IsRequired();

        builder.Property(s => s.BranchId)
            .IsRequired();

        builder.Property(s => s.TotalAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.IsCancelled)
            .IsRequired();

        builder.HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey("SaleId")
            .OnDelete(DeleteBehavior.Cascade);

        // builder.Property(s => s.CreatedAt)
        //     .HasDefaultValueSql("CURRENT_TIMESTAMP")
        //     .ValueGeneratedOnAdd();

        // builder.Property(s => s.UpdatedAt)
        //     .HasDefaultValueSql("CURRENT_TIMESTAMP")
        //     .ValueGeneratedOnAddOrUpdate();
    }
}