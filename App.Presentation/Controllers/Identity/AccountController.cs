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

        public IActionResult Index()
        {
            return View();
        }

     
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList(string search)
        {
            var users = await _identityService.GetAllUsersAsync();
            var usersModel = _mapper.Map<List<ApplicationUserModel>>(users);
            if (!string.IsNullOrEmpty(search))
                usersModel = usersModel.Where(x => x.UserName.Contains(search) || x.Email.Contains(search)).ToList();
            ViewBag.Search = search;
            return View(usersModel);
        }


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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser()
        {
           var Roles =await _identityService.GetAllRolesAsync();
           var AvailableRoles = Roles.Select(r => new SelectListItem
           {
                Value =r.Name,
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
                var selectedRoles = Roles.Where(x=> userModel.SelectedRoles.Contains(x.Name)).ToList();
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
    }
}
