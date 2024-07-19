using Microsoft.AspNetCore.Identity;
using Sakany.Domain.Enumerations.Users;

namespace Sakany.Persistence.DataSeeding.Security.Roles
{
    public static class RolesDataSeeding
    {
        public static async Task InitializeRolesDataSeedingAsync(this RoleManager<IdentityRole> roleManager)
        {
            foreach (string role in Enum.GetNames(typeof(UserRole)))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}