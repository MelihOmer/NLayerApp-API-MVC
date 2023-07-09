using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Entities;

namespace NLayer.Repository.EntityConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Stock).IsRequired();
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);

            //Seed Data
            builder.HasData(
                new Product { Id = 1, CategoryId = 1, Name = "Kalem 1", Price = 100, Stock = 20, CreatedDate = DateTime.Now },
                new Product { Id = 2, CategoryId = 1, Name = "Kalem 2", Price = 200, Stock = 30, CreatedDate = DateTime.Now },
                new Product { Id = 3, CategoryId = 1, Name = "Kalem 3", Price = 600, Stock = 60, CreatedDate = DateTime.Now },
                new Product { Id = 4, CategoryId = 2, Name = "Kitap 1", Price = 600, Stock = 60, CreatedDate = DateTime.Now },
                new Product { Id = 5, CategoryId = 2, Name = "Kitap 2", Price = 520, Stock = 12, CreatedDate = DateTime.Now });
        }
    }
}
