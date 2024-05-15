using AnsarTask.Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace AnsarTask.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
                await roleManager.CreateAsync(new IdentityRole(AppRoles.User));
            }
        }
    }
}
