using LabTestAppointments.Data.Entities;
using LabTestAppointments.Data.Interfaces.Repositories.Generic;

namespace LabTestAppointments.Data.Interfaces.Repositories
{
    public interface IHealthInsuranceRepository : IGenericRepository<HealthInsurance>
    {
        Task<List<HealthInsurance>> GetAllWithIncludeAsync(List<string> properties);
        Task<HealthInsurance> GetByIdWithIncludeAsync(int id, List<string> properties);
    }
}
