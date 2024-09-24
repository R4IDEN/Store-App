using Microsoft.AspNetCore.Identity;
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

        public static void ConfigureLocalization(this WebApplication app) 
        {
            app.UseRequestLocalization(options =>
            options.AddSupportedCultures("tr-TR", "en-US").
            SetDefaultCulture("tr-TR")
            );
        }
    
        public static async void ConfigureDefaultAdminUser(this IApplicationBuilder app) 
        {
            const string adminUser = "Admin";
            const string adminPassword = "Admin+123456";

            /* UserManager
             * Kullanicilar ile calismak icin metotlar saglar.
            */
            UserManager<IdentityUser> userManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

             /* RoleManager
             * Role ile calismak icin metotlar saglar.
             */
             RoleManager<IdentityRole> roleManager = app
                .ApplicationServices
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            IdentityUser user = await userManager.FindByNameAsync(adminUser);
            if (user is null) 
            {
                user = new IdentityUser(adminUser)
                {
                   Email="storeApp@info.com",
                   PhoneNumber="5443456161",
                   EmailConfirmed=true
                };

                var userResult = await userManager.CreateAsync(user, adminPassword);

                if (!userResult.Succeeded)
                    throw new Exception("Admin user couldn't created.");

                var roleResult = await userManager.AddToRolesAsync(user,
                roleManager.Roles.Select(r => r.Name).ToList());

                if (!roleResult.Succeeded)
                    throw new Exception("System has problems with role def.");
            }
        }
    }
}
