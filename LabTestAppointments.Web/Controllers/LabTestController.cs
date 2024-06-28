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

        public async Task<IActionResult> Details(int id)
        {
            return View(await _labTestRepository.GetByIdAsync(id));
        }
        public async Task<IActionResult> Create()
        {
            LabTest labTest = new LabTest();
            return View(labTest);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LabTest labTest)
        {
            try
            {
                labTest.CreatedBy = "DefaultUser";
                labTest.CreatedOn = DateTime.Now;

                await _labTestRepository.AddAsync(labTest);

                return RedirectToRoute(new { controller = "LabTest", action = "Index" });
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}