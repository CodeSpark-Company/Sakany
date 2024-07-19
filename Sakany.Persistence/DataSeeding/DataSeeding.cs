using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sakany.Persistence.DataSeeding.Security.Roles;
using Sakany.Persistence.DbContexts;

namespace Sakany.Persistence.DataSeeding
{
    public static class DataSeeding
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var context = services.GetService<SakanyDbContext>();

            if (context is null)
                return;

            var isPendingMigrations = (await context.Database.GetPendingMigrationsAsync()).Any();

            if (isPendingMigrations)
            {
                await MigrateDatabaseAsync(context);

                #region Initialize Data

                await InitializeRolesDataAsync(services);

                #endregion Initialize Data
            }
        }

        private static async Task MigrateDatabaseAsync(SakanyDbContext context)
        {
            await context.Database.MigrateAsync();
        }

        private static async Task InitializeRolesDataAsync(IServiceProvider services)
        {
            RoleManager<IdentityRole>? roleManager = services.GetService<RoleManager<IdentityRole>>();

            if (roleManager is null)
                return;

            await roleManager.InitializeRolesDataSeedingAsync();
        }
    }
}