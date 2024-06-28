using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LabTestAppointments.Web.Controllers
{
    public class BioanalystController : Controller
    {
        private readonly IBioanalystRepository _bioanalystRepository;
        public BioanalystController(IBioanalystRepository bioanalystRepository)
        {
            _bioanalystRepository = bioanalystRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _bioanalystRepository.GetAllAsync());
        }
    }
}