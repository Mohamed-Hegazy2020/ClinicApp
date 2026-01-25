using App.Application.Shared;
using App.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace App.Application.IServices
{
    public interface IidentityService
    {
        //user
        Task<IdentityResultModel> AddUserAsync(ApplicationUser user,List<ApplicationRole> roles, string password);
        Task<List<ApplicationUserWithRoles>> GetAllUsersWithRolesAsync();
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByIdAsync(int id);
        Task<ApplicationUser> GetUserByNameAsync(string userName);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(ApplicationUser user);
        bool IsSignedInUser(ClaimsPrincipal principal);
        Task<IList<string>> GetUserRolesAsync(ApplicationUser user);
        Task AddUserToRolesAsync(ApplicationUser user ,List<string> roles);
        Task RemoveUserFromRolesAsync(ApplicationUser user ,List<string> roles);




        //log in
        Task<IdentityResultModel> RegisterAsync(ApplicationUser user,string Password,bool IsPersistent);
        Task<IdentityResultModel> LoginAsync(ApplicationUser user, string Password, bool IsPersisten);

        Task LogoutAsync();








        //Roles
        public Task<string> AddRoleAsync(ApplicationRole role);
        public Task UpdateRoleAsync(ApplicationRole role);
        Task<IEnumerable<ApplicationRole>> GetAllRolesAsync();
        Task DeleteRoleAsync(string roleName);

    }
}
