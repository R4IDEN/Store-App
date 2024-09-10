using Microsoft.EntityFrameworkCore;
using StoreApp.Repositories;

namespace BigStoreApp.Infrastructure.Extensions
{
    public static class ApplicationExtension
    {
        public static void ConfigureAndCheckMigration(this IApplicationBuilder app)
        {
            AppDbContext _context = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<AppDbContext>();

            if ( _context.Database.GetPendingMigrations().Any()) 
                _context.Database.Migrate();

        }

        public static void ConfigureLocalization(this IApplicationBuilder app) 
        {
            app.UseRequestLocalization(options =>
            options.AddSupportedCultures("tr-TR", "en-US").
            SetDefaultCulture("tr-TR")
            );
        }
    }
}
