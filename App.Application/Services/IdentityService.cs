using App.Application.IServices;
using App.Application.Shared;
using App.Domain.Entities.Identity;
using App.Domain.Enums;
using App.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace App.Application.Services
{
    public class IdentityService : IidentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly AppDbContext _dbContext;
        //private readonly PasswordHasher<ApplicationUser> _passwordHasher;
        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, SignInManager<ApplicationUser> signInManager,/* PasswordHasher<ApplicationUser>  passwordHasher,*/ AppDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            //_passwordHasher = passwordHasher;
            _signInManager = signInManager;
        }

        #region Users

        public async Task<string> AddUserAsync(ApplicationUser user, List<ApplicationRole> roles, string password)
        {
            var trans = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                //if email is exist
                var existuser = await _userManager.FindByEmailAsync(user.Email);
                //email is exist
                if (existuser != null)
                    return "EmailIsExist";

                //if username is exist
                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                //username is exist
                if (userByUserName != null)
                    return "UserNameIsExist";

                //create
                var CreateResult = await _userManager.CreateAsync(user, password);
                //failed
                if (!CreateResult.Succeeded)
                    return string.Join(",", CreateResult.Errors.Select(x => x.Description).ToList());
                //message

                if (roles == null || roles.Count <= 0)
                    return "SelectUserRoles";

                var userRoles = roles.Select(x => x.Name);
                await _userManager.AddToRolesAsync(user, userRoles);
                await _dbContext.SaveChangesAsync();
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }



        }

        #endregion


        #region Roles
        public async Task<string> AddRoleAsync(ApplicationRole role)
        {            
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
                return "success";
            return "Failed";
        }

        public Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUser> GetUserByNameAsync(string userName)
        {
            var userByUserName = await _userManager.FindByNameAsync(userName);
            return userByUserName;

        }

        public Task UpdateUserAsync(int id, ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityResultModel> RegisterAsync(ApplicationUser user,string Password,bool IsPersistent)
        {
            IdentityResultModel result = new IdentityResultModel();
            try
            {
                var IsExistUser=await _userManager.FindByNameAsync(user.UserName);
                if (IsExistUser!=null)
                {
                    result.Message = "The user is aredy exist";
                    return result;
                }
                else
                {
                    var r= await _userManager.CreateAsync(user, Password);
                    if (r.Succeeded)
                    {
                        IdentityResult roleResult = await _userManager.AddToRoleAsync(user, RolesEnum.User.ToString());
                        await _signInManager.SignInAsync(user, IsPersistent);
                        result.Succeeded = true;
                        result.Message = "Registered successfully";
                        return result;
                    }
                    else
                    {
                        foreach (var item in r.Errors)
                        {
                            result.Message += $"{item.Description} ,";
                        }
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
        }

        public async Task<IdentityResultModel> LoginAsync(ApplicationUser user, string Password, bool IsPersisten)
        {
            IdentityResultModel result = new IdentityResultModel();
            try
            {
                var curuntUser =await _userManager.FindByNameAsync(user.UserName);
                if (curuntUser!=null)
                {
                    bool isPasswordFound = await _userManager.CheckPasswordAsync(curuntUser, Password);
                    if (isPasswordFound)
                    {                               

                        var r = await _signInManager.PasswordSignInAsync(user.UserName, Password, IsPersisten, false);
                        if (r.Succeeded)
                        {
                            result.Succeeded = true;
                            result.Message = "Loged in successfully";
                        }
                        else if (r.IsNotAllowed)
                        {
                            result.Message = "IsNotAllowed";
                        }
                        else if (r.IsLockedOut)
                        {
                            result.Message = "IsLockedOut";
                        }
                        else if (r.IsNotAllowed)
                        {
                            result.Message = "IsNotAllowed";
                        }
                    }
                    else
                    {
                        result.Message = "In valide password";

                    }

                }
                else
                {
                    result.Message = "The user not found";

                }                

                return result;
            }
            catch (Exception)
            {
                result.Message = "Erorr";
                return result;

            }

        }


        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public Task UpdateRoleAsync(ApplicationRole role)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetAllRolesAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }
        public bool IsSignedInUser(ClaimsPrincipal principal)
        {
            var IsSignedInUser = _signInManager.IsSignedIn(principal);
            return IsSignedInUser;
        }
        #endregion

    }
}
