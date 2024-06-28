using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LabTestAppointments.Web.Controllers
{
    public class InsuranceProviderController : Controller
    {
        private readonly IInsuranceProviderRepository _insuranceProviderRepository;
        public InsuranceProviderController(IInsuranceProviderRepository insuranceProviderRepository)
        {
            _insuranceProviderRepository = insuranceProviderRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _insuranceProviderRepository.GetAllAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _insuranceProviderRepository.GetByIdAsync(id));
        }
    }
}