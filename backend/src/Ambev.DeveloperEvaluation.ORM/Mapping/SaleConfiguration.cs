    using Ambev.DeveloperEvaluation.Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    namespace Ambev.DeveloperEvaluation.ORM.Mapping
    {
        public class SaleConfiguration : IEntityTypeConfiguration<Sale>
        {
            public void Configure(EntityTypeBuilder<Sale> builder)
            {
                builder.ToTable("Sales");

                builder.HasKey(s => s.Id);

                builder.Property(s => s.SaleNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                builder.Property(s => s.CustomerName)
                    .IsRequired()
                    .HasMaxLength(200);

                builder.Property(s => s.BranchName)
                    .IsRequired()
                    .HasMaxLength(200);
                
                builder.Property(s => s.TotalAmount)
                    .HasColumnType("decimal(18,2)");

                // Defines the one-to-many relationship.
                // When a Sale is deleted, all its associated Items are also deleted.
                builder.HasMany(s => s.Items)
                    .WithOne()
                    .HasForeignKey("SaleId") // Shadow property
                    .OnDelete(DeleteBehavior.Cascade);
            }
        }
    }
    