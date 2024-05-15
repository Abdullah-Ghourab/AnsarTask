using AnsarTask.Core.Constants;
using Microsoft.AspNetCore.Identity;

namespace AnsarTask.Seeds
{
    public class DefaultUsers
    {
        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            IdentityUser Admin = new()
            {
                UserName = "Admin@Ansar.com",
                Email = "Admin@Ansar.com",
                EmailConfirmed = true
            };
            IdentityUser User = new()
            {
                UserName = "User@Ansar.com",
                Email = "User@Ansar.com",
                EmailConfirmed = true
            };
            await CreateUser(userManager, User, AppRoles.User, "P@ssword123");
            await CreateUser(userManager, Admin, AppRoles.Admin, "P@ssword123");
        }
        private static async Task CreateUser(UserManager<IdentityUser> userManager, IdentityUser userToCreate, string role, string password)
        {
            var user = await userManager.FindByNameAsync(userToCreate.UserName!);
            if (user is null)
            {
                await userManager.CreateAsync(userToCreate, password);
                await userManager.AddToRoleAsync(userToCreate, role);
            }
        }
    }
}
