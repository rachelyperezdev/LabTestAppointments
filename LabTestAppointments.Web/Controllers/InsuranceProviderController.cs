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
        public async Task<IActionResult> Create()
        {
            InsuranceProvider insuranceProvider = new InsuranceProvider();
            return View(insuranceProvider);
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(InsuranceProvider insuranceProvider)
        {
            try
            {
                insuranceProvider.CreatedBy = "DefaultUser";
                insuranceProvider.CreatedOn = DateTime.Now;

                await _insuranceProviderRepository.AddAsync(insuranceProvider);

                return RedirectToRoute(new { controller = "InsuranceProvider", action = "Index" });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _insuranceProviderRepository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InsuranceProvider insuranceProvider)
        {
            try
            {
                insuranceProvider.CreatedBy = "DefaultUser";
                insuranceProvider.CreatedOn = DateTime.Now;
                insuranceProvider.ModifiedBy = "DefaultUser";
                insuranceProvider.ModifiedOn = DateTime.Now;

                await _insuranceProviderRepository.UpdateAsync(insuranceProvider, insuranceProvider.Id);

                return RedirectToRoute(new { controller = "InsuranceProvider", action = "Index" });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

    }
}