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
    }
}
