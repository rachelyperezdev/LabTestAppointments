using LabTestAppointments.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabTestAppointments.Data.Contexts
{
    public class LabTestAppointmentsContext : DbContext
    {
        public LabTestAppointmentsContext(DbContextOptions<LabTestAppointmentsContext> options) : base(options)
        {
            
        }
        DbSet<InsuranceProvider> InsuranceProviders { get; set; }
    }
}
