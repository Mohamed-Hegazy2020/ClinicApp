using App.Application.IServices;
using App.Domain.Entities.Basics;
using App.Presentation.Models.Basics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.Presentation.Controllers.Basics
{
    [Authorize(Roles = "Admin")]
    public class ClinicController : Controller
    {
        private readonly IClinicService _ClinicService;
        private readonly IMapper _mapper;

        public ClinicController(IClinicService ClinicService, IMapper mapper)
        {
            _ClinicService = ClinicService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ClinicModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClinicModel ClinicModel)
        {
            if (ClinicModel != null)
            {
                var Clinic= _mapper.Map<Clinic>(ClinicModel);    
                var result=await _ClinicService.AddClinicAsync(Clinic);
                if (result>0)
                {
                    return View(ClinicModel);
                }
            }
                
            return View(ClinicModel);
        }
    }
}
