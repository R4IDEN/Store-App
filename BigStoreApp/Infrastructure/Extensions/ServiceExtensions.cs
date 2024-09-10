using BigStoreApp.Models;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Concretes;
using Repositories.Contracts;
using Services.Concretes;
using Services.Contracts;
using StoreApp.Repositories;

namespace BigStoreApp.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("SqlConnection"), options =>
                {
                    options.MigrationsAssembly("BigStoreApp");
                    //options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                });
            });
        }

        public static void ConfigureSession(this IServiceCollection services)
        {
            //Bu ikisi session'lar icin
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.Cookie.Name = "StoreApp.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<Cart>(c => SessionCart.GetCart(c));
        }

        public static void ConfigureRepositoryRegistration(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        public static void ConfigureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
        }
        public static void ConfigureRouting(this IServiceCollection services)
        {
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });
        }
    }
}
