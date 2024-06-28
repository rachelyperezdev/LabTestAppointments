using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LabTestAppointments.Web.Controllers
{
    public class MedicalImageController : Controller
    {
        private readonly IMedicalImageRepository _medicalImageRepository;

        public MedicalImageController(IMedicalImageRepository medicalImageRepository)
        {
            _medicalImageRepository = medicalImageRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _medicalImageRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _medicalImageRepository.GetByIdAsync(id));
        }

    }
}