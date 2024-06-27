using LabTestAppointments.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointments.Data.Contexts
{
    public class LabTestAppointmentsContext : DbContext
    {
        public LabTestAppointmentsContext(DbContextOptions<LabTestAppointmentsContext> options) : base(options)
        {
           
        }
        DbSet<LabTest> LabTests { get; set; }
        DbSet<MedicalImage> MedicalImages { get; set; }
        DbSet<Bioanalyst> Bioanalyst { get; set; }
        DbSet<InsuranceProvider> InsuranceProviders { get; set; }
        DbSet<HealthInsurance> HealthInsurances { get; set; }

    }
}