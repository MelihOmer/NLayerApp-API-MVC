using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Entities;

namespace NLayer.Repository.EntityConfiguration
{
    internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(pf => pf.Product).WithOne(p => p.ProductFeature).HasForeignKey<ProductFeature>(pf => pf.ProductId);

            //Seed Data
            builder.HasData(
                new ProductFeature { Id = 1, Color = "Kırmızı", Height = 100, Width = 200, ProductId = 1 },
                new ProductFeature { Id = 2, Color = "Mavi", Height = 300, Width = 450, ProductId = 2 });
        }
    }
}
