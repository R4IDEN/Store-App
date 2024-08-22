using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Config;
using System.Reflection;

namespace StoreApp.Repositories
{
    public class AppDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.ApplyConfiguration(new CategoryConfig());
            //modelBuilder.ApplyConfiguration(new ProductConfig());

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
