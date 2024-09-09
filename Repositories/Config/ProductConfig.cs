
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p=>p.ProductId);

            builder.HasData(
                new Product()
                {
                    ProductId = 1,
                    Name = "Sabun",
                    Price = 199,
                    ImageUrl = "sabun.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Parfüm",
                    Price = 5000_55,
                    ImageUrl = "parfum.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductId = 3,
                    Name = "Ruj",
                    Price = 15_40,
                    ImageUrl = "ruj.jpg",
                    CategoryId = 1,
                    Showcase = true
                },
                new Product()
                {
                    ProductId = 4,
                    Name = "Monitor",
                    Price = 2132_99,
                    ImageUrl = "monitor.jpg",
                    CategoryId = 2,
                    Showcase = true
                });
        }
    }
}
