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

        public async Task<IActionResult> Details(int id)
        {
            return View(await _bioanalystRepository.GetByIdAsync(id));
        }

        public async Task<IActionResult> Create()
        {
            Bioanalyst bioanalyst = new Bioanalyst();
            return View(bioanalyst);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bioanalyst bioanalyst)
        {
            try
            {
                bioanalyst.CreatedBy = "DefaultUser";
                bioanalyst.CreatedOn = DateTime.Now;

                await _bioanalystRepository.AddAsync(bioanalyst);

                return RedirectToRoute(new { controller = "Bioanalyst", action = "Index" });
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public async Task<IActionResult> Edit(int id)
        {
            return View(await _bioanalystRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Bioanalyst bioanalyst)
        {
            try
            {
                bioanalyst.CreatedBy = "DefaultUser";
                bioanalyst.CreatedOn = DateTime.Now;
                bioanalyst.ModifiedBy = "DefaultUser";
                bioanalyst.ModifiedOn = DateTime.Now;

                await _bioanalystRepository.UpdateAsync(bioanalyst, bioanalyst.Id);

                return RedirectToRoute(new { controller = "Bioanalyst", action = "Index" });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}