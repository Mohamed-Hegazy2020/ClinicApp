using App.Application.IServices;
using App.Application.Services;
using App.Domain.Entities.Identity;
using App.Presentation.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

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
                return View();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _identityService.LogoutAsync();
            return RedirectToAction("Index", "Home");

        }
    }
}
