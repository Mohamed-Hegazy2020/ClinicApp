using App.Domain.Entities.Identity;
using App.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<ApplicationRole> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.CountAsync();
            if (rolesCount <= 0)
            {
                await _roleManager.CreateAsync(new ApplicationRole()
                {
                    Name = RolesEnum.Admin.ToString(), 
                    NameAr="مدير النظام", 
                    IsSystemRole=true

                });
                await _roleManager.CreateAsync(new ApplicationRole()
                {
                    Name = RolesEnum.User.ToString(),
                    NameAr = "مستخدم"
                   

                });


            }
        }
    }
}
