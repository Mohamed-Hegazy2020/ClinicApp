using App.Domain.Entities.Identity;
using App.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> _userManager)
        {
            var usersIsCount = await _userManager.Users.CountAsync();
            if (usersIsCount <= 0)
            {
                var defaultuser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    Country = "Egypt",
                };
                await _userManager.CreateAsync(defaultuser, "123");
                await _userManager.AddToRoleAsync(defaultuser, RolesEnum.Admin.ToString());
            }
        }
    }
}
