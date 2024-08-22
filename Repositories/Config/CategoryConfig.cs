
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Repositories.Config
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            //builder.HasKey(x => x.CategoryId);

            builder.HasData(
                new Category() { CategoryId = 1, Name = "Cleaning Product" },
                new Category() { CategoryId = 2, Name = "Technology Products" });
        }
    }
}
