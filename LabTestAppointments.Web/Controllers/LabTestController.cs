using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LabTestAppointments.Web.Controllers
{
    public class LabTestController : Controller
    {
        private readonly ILabTestRepository _labTestRepository;
        public LabTestController(ILabTestRepository labTestRepository)
        {
            _labTestRepository = labTestRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _labTestRepository.GetAllAsync());
        }
    }
}