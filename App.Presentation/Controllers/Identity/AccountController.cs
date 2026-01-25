using App.Application.Enums;
using App.Application.IServices;
using App.Application.Services;
using App.Domain.Entities.Identity;
using App.Presentation.Helpers;
using App.Presentation.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Presentation.Controllers.Identity
{
    public class AccountController : Controller
    {
        private readonly IidentityService _identityService;
        private readonly IMapper _mapper;
        public AccountController(IidentityService identityService, IMapper mapper)
        {
            _identityService = identityService;
            _mapper = mapper;

        }

        #region User
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList(string search,string msg)
        {
            var usersWithRoles = await _identityService.GetAllUsersWithRolesAsync();
            if (!string.IsNullOrEmpty(search))
            {
                usersWithRoles = usersWithRoles
                    .Where(u => u.User.UserName.Contains(search, StringComparison.OrdinalIgnoreCase)|| u.User.Email.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var usersModel = usersWithRoles.Select(u =>
            {
                var model = _mapper.Map<ApplicationUserModel>(u.User);
                //model.SelectedRoles = u.Roles;
                model.RolesDisplay = string.Join(", ", u.Roles);
                return model;
            }).ToList();

            ViewBag.Search = search;
            if (!string.IsNullOrEmpty(msg))
            {
                ViewData["NotificationMsg"] = Notification.Erorr(msg);
            }
            return View(usersModel);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser()
        {
            var Roles = await _identityService.GetAllRolesAsync();
            var AvailableRoles = Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();
            ApplicationUserModel ApplicationUserModel = new ApplicationUserModel();
            ApplicationUserModel.AvailableRoles = AvailableRoles;
            return View(ApplicationUserModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser(ApplicationUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(userModel);
                var Roles = await _identityService.GetAllRolesAsync();
                var selectedRoles = Roles.Where(x => userModel.SelectedRoles.Contains(x.Name)).ToList();
                var result = await _identityService.AddUserAsync(user, selectedRoles, userModel.Password);
                var AvailableRoles = Roles.Select(r => new SelectListItem
                {
                    Value = r.Name,
                    Text = r.Name
                }).ToList();
                userModel.AvailableRoles = AvailableRoles;
                if (result.Succeeded)
                {
                    ViewData["NotificationMsg"] = Notification.Success("User created successfully");
                    return View(userModel);
                }
                else
                {

                    ModelState.AddModelError("", result.Message);
                }
            }
            return View(userModel);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _identityService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var model = _mapper.Map<ApplicationUserModel>(user);

            var userRoles = await _identityService.GetUserRolesAsync(user);
            var roles = await _identityService.GetAllRolesAsync();

            model.SelectedRoles = userRoles.ToList();
            model.AvailableRoles = roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(ApplicationUserModel model)
        {
            // Reload roles if model is invalid to redisplay the view
            var roles = await _identityService.GetAllRolesAsync();
            model.AvailableRoles = roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();
            if (!ModelState.IsValid)
            {                
                return View(model);
            }

            // Get the user from database
            var user = await _identityService.GetUserByIdAsync(model.Id);
            if (user == null)
                return NotFound();

            // Update basic fields
            //_mapper.Map(model, user); // Map updated properties from model to entity

            // Update roles
            var currentRoles = await _identityService.GetUserRolesAsync(user);
            var rolesToAdd = model.SelectedRoles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(model.SelectedRoles).ToList();

            if (rolesToAdd.Any())
                await _identityService.AddUserToRolesAsync(user, rolesToAdd);

            if (rolesToRemove.Any())
                await _identityService.RemoveUserFromRolesAsync(user, rolesToRemove);

            user.UserName = model.UserName;             
            user.Email = model.Email;             

            // Save changes
            await _identityService.UpdateUserAsync(user);
            ViewData["NotificationMsg"] = Notification.Success("User updated successfully");
            return View(model);
            //return RedirectToAction("UserList");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // 1. Find the user
            string msg = "";
            var user = await _identityService.GetUserByIdAsync(id);
            if (user == null)
            {
                //ViewData["NotificationMsg"] = Notification.Erorr("User not found.");
                msg = "User not found.";
                return RedirectToAction("UserList", new { msg = msg });
            }

            // 2. Prevent deleting yourself (optional)
            if (user.UserName == User.Identity!.Name)
            {
                //ViewData["NotificationMsg"] = Notification.Erorr("You cannot delete your own account.");
                msg = "You cannot delete your own account.";
                return RedirectToAction("UserList", new { msg = msg });
            }

            // 3. Delete user
            try
            {
                await _identityService.DeleteUserAsync(user);
                msg = "User deleted successfully.";
                //ViewData["NotificationMsg"] = Notification.Success("User deleted successfully.");
            }
            catch (Exception ex)
            {    
                msg = $"Error deleting user: {ex.Message}";
                //ViewData["NotificationMsg"] = Notification.Erorr($"Error deleting user: {ex.Message}");
            }

            return RedirectToAction("UserList",new {msg = msg });
        }


        #endregion





        #region Login

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<ApplicationUser>(userModel);
                var result = await _identityService.RegisterAsync(user, userModel.Password, userModel.IsPersistent);
                if (result.Succeeded)
                {
                    //return RedirectToAction("Index", "Home");
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index", "HomeAdmin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                }




            }


            return View();
        }
        public async Task<IActionResult> LogIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(ApplicationUserModel userModel)
        {
            var user = _mapper.Map<ApplicationUser>(userModel);

            var result = await _identityService.LoginAsync(user, userModel.Password, userModel.IsPersistent);
            if (result.Succeeded)
            {
                return RedirectToAction("Dashpoard", "Home");
                //if (User.IsInRole("Admin"))
                //{
                //    return RedirectToAction("Dashpoard", "Home");
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }
            else
            {
                ModelState.AddModelError("", result.Message);
                return View();
            }

        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _identityService.LogoutAsync();
            return RedirectToAction("LogIn", "Account");

        } 
        #endregion



    }
}
