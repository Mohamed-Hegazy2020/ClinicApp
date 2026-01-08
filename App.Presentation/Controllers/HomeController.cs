using App.Application.IServices;
using App.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IidentityService _IdentityService;
        public HomeController(IidentityService identityService,ILogger<HomeController> logger)
        {
            _logger = logger;
            _IdentityService = identityService;

        }

        public IActionResult Index()
        {
            return View();
            //if (TempData["NotificationMsg"]!=null)
            //{
            //    TempData.Keep("NotificationMsg");
            //}
            //if (_IdentityService.IsSignedInUser(User))
            //{
            //    if (User.IsInRole("Admin"))
            //    {
            //        return RedirectToAction("Index", "HomeAdmin");
            //    }
            //    else
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //}
            //else
            //{
            //    return View();
            //}


        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
