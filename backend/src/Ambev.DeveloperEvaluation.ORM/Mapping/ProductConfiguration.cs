using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Category)
                .HasMaxLength(100);

            builder.OwnsOne(p => p.Rating, ratingBuilder =>
            {
                ratingBuilder.Property(r => r.Rate).HasColumnName("Rating_Rate");
                ratingBuilder.Property(r => r.Count).HasColumnName("Rating_Count");
            });
        }
    }
}
