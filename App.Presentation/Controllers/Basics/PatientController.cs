using App.Application.Enums;
using App.Application.IServices;
using App.Application.Services;
using App.Domain.Entities.Basics;
using App.Presentation.Extentions;
using App.Presentation.Helpers;
using App.Presentation.Models.Basics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Controllers.Basics
{
	[Authorize(Roles = "Admin")]
	public class PatientController : Controller
	{
		private readonly IPatientService _PatientService;
		private readonly LocalizationService _LocalizationService;
		private readonly IMapper _mapper;
		public PatientController(IPatientService PatientService, LocalizationService localizationService, IMapper mapper)
		{
			_PatientService = PatientService;
			_LocalizationService = localizationService;
			_mapper = mapper;

		}

		public void IntializeDropdowens()
		{
			
			ViewBag.Gender = PresentationExtensions.ConvertEnumToSelectListItems(typeof(Gender), _LocalizationService);
			ViewBag.BloodType = PresentationExtensions.ConvertEnumToSelectListItems(typeof(BloodType), _LocalizationService);

		}
		public async Task<IActionResult> List(string search, string msg)
		{
			var Patients =  _PatientService.GetAllPatientsAsNoTracking();
			if (!string.IsNullOrEmpty(search))
			{
				Patients = Patients.Where(x => x.NameAr.Contains(search) || x.NameEn.Contains(search));
			}
			var PatientsModel = _mapper.Map<List<PatientModel>>(Patients);
			ViewBag.Search = search;
			if (!string.IsNullOrEmpty(msg))
			{
				ViewData["NotificationMsg"] = Notification.Erorr(msg);
			}
			return View(PatientsModel);
		}

		[HttpGet]
		public async Task<IActionResult> Create()
		{
			IntializeDropdowens();
			return View(new PatientModel() { Code = await _PatientService.GetNewCodeAsync() });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(PatientModel PatientModel)
		{
			if (ModelState.IsValid)
			{
				var patient = _mapper.Map<Patient>(PatientModel);			
				var result = await _PatientService.AddPatientAsync(patient);

				if (result>0)
				{
					ViewData["NotificationMsg"] = Notification.Success("Patient created successfully");					
				}
				else
				{
					ViewData["NotificationMsg"] = Notification.Erorr("Patient not created");
				}
			}
			IntializeDropdowens();

			return View(PatientModel);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var patient =await _PatientService.GetPatientByID(id);
			if (patient != null)
			{
				IntializeDropdowens();
				var patientModel = _mapper.Map<PatientModel>(patient);
				return View(patientModel);	
			}

			return RedirectToAction("List",new {msg= "Patient not found" });
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(PatientModel PatientModel)
		{
			IntializeDropdowens();
			if (ModelState.IsValid)
			{

				Patient patient = _mapper.Map<Patient>(PatientModel);
				int result = await _PatientService.UpdatePatientAsync(patient);
				if (result > 0)
				{
					ViewData["NotificationMsg"] = Notification.Success("Patient updated successfully");
					return View(PatientModel);
				}
				else
				{
					ViewData["NotificationMsg"] = Notification.Erorr("Patient not updated");
					return View(PatientModel);
				}

			}
			var modelErrors = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors.SelectMany(s => s.ErrorMessage))) ;
			ViewData["NotificationMsg"] = Notification.Erorr(modelErrors);
			return View(PatientModel);


		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var patient = await _PatientService.GetPatientByID(id);
			if (patient != null)
			{
				int result =await _PatientService.DeletePatientAsync(patient);
				if(result > 0)
				{
					return RedirectToAction("List", new { msg = "Patient deleted successfully" });
				}
			
			}

			return RedirectToAction("UserList", new { msg = "Patient not updated" });
		}




	}
}
