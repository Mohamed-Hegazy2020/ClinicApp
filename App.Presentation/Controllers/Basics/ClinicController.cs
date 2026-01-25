using App.Application.IServices;
using App.Application.Services;
using App.Domain.Entities.Basics;
using App.Presentation.Helpers;
using App.Presentation.Models.Basics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace App.Presentation.Controllers.Basics
{
    [Authorize(Roles = "Admin")]
    public class ClinicController : Controller
    {
        private readonly IClinicService _ClinicService;
        private readonly IMapper _mapper;
		private readonly IHostingEnvironment _Hosting;
		public ClinicController(IClinicService ClinicService, IMapper mapper, IHostingEnvironment hosting)
        {
            _ClinicService = ClinicService;
            _mapper = mapper;
			_Hosting = hosting;
		}


		public async Task<IActionResult> List(string search, string msg)
		{
			var Clinics = _ClinicService.GetAllClinicsAsNoTracking();
			ViewBag.AllowAddNew = Clinics.Count() == 0;
			if (!string.IsNullOrEmpty(search))
			{
				Clinics = Clinics.Where(x => x.NameAr.Contains(search) || x.NameEn.Contains(search));
			}
			var ClinicsModel = _mapper.Map<List<ClinicModel>>(Clinics);
			ViewBag.Search = search;
			if (!string.IsNullOrEmpty(msg))
			{
				ViewData["NotificationMsg"] = Notification.Erorr(msg);
			}
			return View(ClinicsModel);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{		
			return View(new ClinicModel() { });
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ClinicModel ClinicModel)
		{
			if (ModelState.IsValid)
			{
				var Clinic = _mapper.Map<Clinic>(ClinicModel);
				string ImagePath = ImageUploader.UploadImage(ClinicModel.ImageFile, _Hosting);
				ClinicModel.ImagePath = Clinic.ImagePath = !string.IsNullOrEmpty(ImagePath) ? ImagePath : Clinic.ImagePath;
				var result = await _ClinicService.AddClinicAsync(Clinic);

				if (result > 0)
				{
					ViewData["NotificationMsg"] = Notification.Success("Clinic created successfully");
				}
				else
				{
					ViewData["NotificationMsg"] = Notification.Erorr("Clinic not created");
				}
			}
			

			return View(ClinicModel);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var Clinic = await _ClinicService.GetClinicByID(id);
			if (Clinic != null)
			{
				var ClinicModel = _mapper.Map<ClinicModel>(Clinic);
				return View(ClinicModel);
			}

			return RedirectToAction("List", new { msg = "Clinic not found" });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(ClinicModel ClinicModel)
		{
			if (ModelState.IsValid)
			{

				Clinic Clinic = _mapper.Map<Clinic>(ClinicModel);
				string ImagePath = ImageUploader.UploadImage(ClinicModel.ImageFile, _Hosting);
				ClinicModel.ImagePath = Clinic.ImagePath = !string.IsNullOrEmpty(ImagePath) ? ImagePath : Clinic.ImagePath;
				int result = await _ClinicService.UpdateClinicAsync(Clinic);
				if (result > 0)
				{
					ViewData["NotificationMsg"] = Notification.Success("Clinic updated successfully");
					return View(ClinicModel);
				}
				else
				{
					ViewData["NotificationMsg"] = Notification.Erorr("Clinic not updated");
					return View(ClinicModel);
				}

			}
			var modelErrors = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors.SelectMany(s => s.ErrorMessage)));
			ViewData["NotificationMsg"] = Notification.Erorr(modelErrors);
			return View(ClinicModel);


		}






	}
}
