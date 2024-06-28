using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LabTestAppointments.Web.Controllers
{
    public class HealthInsuranceController : Controller
    {
        private readonly IHealthInsuranceRepository _healthInsuranceRepository;
        private readonly IInsuranceProviderRepository _insuranceProviderRepository;
        public HealthInsuranceController(IHealthInsuranceRepository healthInsuranceRepository, IInsuranceProviderRepository insuranceProviderRepository)
        {
            _healthInsuranceRepository = healthInsuranceRepository;
            _insuranceProviderRepository = insuranceProviderRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _healthInsuranceRepository.GetAllWithIncludeAsync(new List<string>() { "InsuranceProvider" }));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _healthInsuranceRepository.GetByIdWithIncludeAsync(id, new List<string>() { "InsuranceProvider" }));
        }

        public async Task<IActionResult> Create()
        {
            HealthInsurance healthInsurance = new HealthInsurance();
            ViewBag.InsuranceProviders = await _healthInsuranceRepository.GetAllWithIncludeAsync(new List<string>() { "InsuranceProvider" });

            return View(healthInsurance);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HealthInsurance healthInsurance)
        {
            try
            {
                healthInsurance.CreatedBy = "DefaultUser";
                healthInsurance.CreatedOn = DateTime.Now;

                await _healthInsuranceRepository.AddAsync(healthInsurance);

                return RedirectToRoute(new { controller = "HealthInsurance", action = "Index" });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.InsuranceProviders = await _healthInsuranceRepository.GetAllWithIncludeAsync(new List<string>() { "InsuranceProvider" });

            return View(await _healthInsuranceRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HealthInsurance healthInsurance)
        {
            try
            {
                healthInsurance.CreatedBy = "DefaultUser";
                healthInsurance.CreatedOn = DateTime.Now;
                healthInsurance.ModifiedBy = "DefaultUser";
                healthInsurance.ModifiedOn = DateTime.Now;

                await _healthInsuranceRepository.UpdateAsync(healthInsurance, healthInsurance.Id);

                return RedirectToRoute(new { controller = "HealthInsurance", action = "Index" });
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
