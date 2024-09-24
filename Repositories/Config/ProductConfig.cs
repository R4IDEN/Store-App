
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
                    ImageUrl = "_photos/sabun.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductId = 2,
                    Name = "Parfüm",
                    Price = 5000_55,
                    ImageUrl = "_photos/parfum.jpg",
                    CategoryId = 1
                },
                new Product()
                {
                    ProductId = 3,
                    Name = "Ruj",
                    Price = 15_40,
                    ImageUrl = "_photos/ruj.jpg",
                    CategoryId = 1,
                    Showcase = true
                },
                new Product()
                {
                    ProductId = 4,
                    Name = "Monitor",
                    Price = 2132_99,
                    ImageUrl = "_photos/monitor.jpg",
                    CategoryId = 2,
                    Showcase = true
                });
        }
    }
}
